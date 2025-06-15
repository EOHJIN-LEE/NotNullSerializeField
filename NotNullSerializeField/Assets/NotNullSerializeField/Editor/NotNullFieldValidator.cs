using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System; // NonSerializedAttribute를 위해 추가

namespace JinStudio.NotNull
{
    public class NotNullFieldValidator
    {
        public ValidationResult ValidateAllOpenScenes()
        {
            var result = new ValidationResult();
            var sceneCount = SceneManager.sceneCount;
            for (var i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded && scene.IsValid())
                {
                    ValidateScene(scene, result);
                }
            }
            return result;
        }

        public ValidationResult ValidateAllScenesInBuildSettings()
        {
            var scenesInBuild = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Select(s => s.path)
                .ToList();
            return ValidateScenesFromPathList(scenesInBuild);
        }

        public ValidationResult ValidateAllProjectScenes()
        {
            var sceneGuids = AssetDatabase.FindAssets("t:Scene");
            var allScenePaths = sceneGuids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.StartsWith("Assets/"))
                .ToList();
            return ValidateScenesFromPathList(allScenePaths);
        }

        private ValidationResult ValidateScenesFromPathList(List<string> scenePaths)
        {
            var finalResult = new ValidationResult();
            if (scenePaths.Count == 0)
            {
                Debug.LogWarning(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Warning_NoScenesToValidate));
                return finalResult;
            }

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                finalResult.IsCancelled = true;
                return finalResult;
            }

            var originalSceneSetup = EditorSceneManager.GetSceneManagerSetup();
            var progressBarTitle = NotNullLocalization.Get(NotNullLocalization.StringKey.ProgressBar_Title);
            var progressBarInfoFormat = NotNullLocalization.Get(NotNullLocalization.StringKey.ProgressBar_Info);

            try
            {
                for (var i = 0; i < scenePaths.Count; i++)
                {
                    var scenePath = scenePaths[i];
                    var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                    var progressBarInfo = string.Format(progressBarInfoFormat, sceneName);
                    EditorUtility.DisplayProgressBar(progressBarTitle, progressBarInfo, (float)i / scenePaths.Count);
                    var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
                    ValidateScene(scene, finalResult);
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                EditorSceneManager.RestoreSceneManagerSetup(originalSceneSetup);
            }
            return finalResult;
        }

        private void ValidateScene(Scene scene, ValidationResult result)
        {
            var allBehaviours = scene.GetRootGameObjects()
                .SelectMany(rootObj => rootObj.GetComponentsInChildren<MonoBehaviour>(true))
                .Where(behaviour => behaviour != null);

            foreach (var behaviour in allBehaviours)
            {
                ValidateFieldsOfBehaviour(behaviour, scene, result);
            }
        }

        /// <summary>
        /// 단일 MonoBehaviour의 모든 필드를 검사하는 '하이브리드' 방식의 최종 메서드입니다.
        /// </summary>
        private void ValidateFieldsOfBehaviour(MonoBehaviour behaviour, Scene scene, ValidationResult result)
        {
            // SerializedObject를 생성하여 직렬화된 데이터에 접근합니다.
            var serializedObject = new SerializedObject(behaviour);
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = behaviour.GetType().GetFields(flags);

            foreach (var field in fields)
            {
                // [NotNull] 속성이 없으면 건너뜁니다.
                if (!field.IsDefined(typeof(NotNull), false)) continue;
                // Unity 오브젝트 타입이 아니면 건너뜁니다.
                if (!typeof(UnityEngine.Object).IsAssignableFrom(field.FieldType)) continue;

                bool isSerialized = (field.IsPublic && !field.IsDefined(typeof(NonSerializedAttribute), false)) ||
                                    field.IsDefined(typeof(SerializeField), true);

                bool isNull = false;
                
                // 필드가 직렬화 대상인지 여부에 따라 다른 방식으로 null을 체크합니다.
                if (isSerialized)
                {
                    // 직렬화 필드: SerializedProperty를 통해 값을 확인 (가장 안정적)
                    var property = serializedObject.FindProperty(field.Name);
                    if (property != null && property.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (property.objectReferenceValue == null)
                        {
                            isNull = true;
                        }
                    }
                }
                else
                {
                    // 비직렬화 필드: 리플렉션을 통해 값을 확인
                    if (field.GetValue(behaviour) == null)
                    {
                        isNull = true;
                    }
                }

                // isNull 플래그를 사용하여 최종적으로 에러를 처리합니다.
                if (isNull)
                {
                    ProcessValidationError(field, behaviour, scene, result);
                }
            }
        }

        private void ProcessValidationError(FieldInfo field, MonoBehaviour behaviour, Scene scene, ValidationResult result)
        {
            object[] parameters = {
                scene.name,
                behaviour.gameObject.name,
                behaviour.GetType().Name,
                field.Name
            };

            var message = string.Format(
                NotNullLocalization.Get(NotNullLocalization.StringKey.Error_NotNullViolation_WithHierarchy),
                parameters
            );
            
            Debug.LogError(message, behaviour);
            
            result.Errors.Add(new ValidationError(
                NotNullLocalization.StringKey.Error_NotNullViolation_WithHierarchy,
                scene.path,
                GetHierarchyPath(behaviour.transform),
                behaviour.GetType().Name,
                parameters
            ));
        }

        private string GetHierarchyPath(Transform t)
        {
            if (t == null) return "";
            if (t.parent == null) return t.name;

            var pathBuilder = new StringBuilder(t.name);
            while (t.parent != null)
            {
                t = t.parent;
                pathBuilder.Insert(0, t.name + "/");
            }
            return pathBuilder.ToString();
        }
    }
}