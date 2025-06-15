using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace JinStudio.NotNull
{
    public class NotNullFieldValidator
    {
        public ValidationResult ValidateAllOpenScenes()
        {
            var result = new ValidationResult();
            for (var i = 0; i < SceneManager.sceneCount; i++)
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

            var progressBarTitle = NotNullLocalization.Get(NotNullLocalization.StringKey.ProgressBar_Title);
            var progressBarInfoFormat = NotNullLocalization.Get(NotNullLocalization.StringKey.ProgressBar_Info);
            
            try //기존의 씬의 데이터를 놔두기 위해서 Additive로 체크
            {
                for (var i = 0; i < scenePaths.Count; i++)
                {
                    var scenePath = scenePaths[i];
                    var sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                    var progressBarInfo = string.Format(progressBarInfoFormat, sceneName);
                    EditorUtility.DisplayProgressBar(progressBarTitle, progressBarInfo, (float)i / scenePaths.Count);
                    
                    var scene = SceneManager.GetSceneByPath(scenePath);
                    var wasAlreadyLoaded = scene.isLoaded;
                    
                    if (!wasAlreadyLoaded)
                    {
                        scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
                    }

                    ValidateScene(scene, finalResult);
                    
                    if (!wasAlreadyLoaded)
                    {
                        EditorSceneManager.CloseScene(scene, true);
                    }
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
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

        private void ValidateFieldsOfBehaviour(MonoBehaviour behaviour, Scene scene, ValidationResult result)
        {
            var serializedObject = new SerializedObject(behaviour);
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var fields = behaviour.GetType().GetFields(flags);

            foreach (var field in fields)
            {
                if (!field.IsDefined(typeof(NotNull), false)) continue;
                if (!typeof(UnityEngine.Object).IsAssignableFrom(field.FieldType)) continue;

                var isSerialized = (field.IsPublic && !field.IsDefined(typeof(NonSerializedAttribute), false)) ||
                                    field.IsDefined(typeof(SerializeField), true);

                var isNull = false;
                
                if (isSerialized)
                {
                    var property = serializedObject.FindProperty(field.Name);
                    if (property != null && property.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (property.objectReferenceValue == null) isNull = true;
                    }
                }
                else
                {
                    if (field.GetValue(behaviour) == null) isNull = true;
                }

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