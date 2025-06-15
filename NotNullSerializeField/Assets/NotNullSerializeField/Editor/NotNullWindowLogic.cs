using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace JinStudio.NotNull
{
    /// <summary>
    /// NotNullValidatorWindow의 모든 비즈니스 로직을 처리하는 클래스입니다.
    /// </summary>
    public class NotNullWindowLogic
    {
        private NotNullFieldValidator _validator = new NotNullFieldValidator();
        private NotNullValidatorWindow _window;

        public NotNullWindowLogic(NotNullValidatorWindow window)
        {
            _window = window;
        }
        
        /// <summary>
        /// 실제 검증을 실행하고 결과를 처리합니다.
        /// </summary>
        public void ExecuteValidation()
        {
            ValidationResult result = null;
            switch (NotNullValidatorSettings.Scope)
            {
                case ValidationScope.CurrentOpenScenes:
                    result = _validator.ValidateAllOpenScenes();
                    break;
                case ValidationScope.ScenesInBuildSettings:
                    result = _validator.ValidateAllScenesInBuildSettings();
                    break;
                case ValidationScope.AllProjectScenes:
                    result = _validator.ValidateAllProjectScenes();
                    break;
            }
            
            _window.DisplayValidationResult(result);

            if (result != null && !result.IsCancelled)
            {
                if (result.IsValid)
                {
                    Debug.Log(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Log_ValidationSuccess));
                }
                else
                {
                    Debug.LogError(string.Format(
                        NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Log_ValidationFailed),
                        result.Errors.Count));
                }
            }
        }

        /// <summary>
        /// '추적' 버튼을 눌렀을 때, 에러가 발생한 오브젝트로 이동합니다.
        /// </summary>
        public void GoToErrorObject(ValidationError error)
        {
            if (EditorSceneManager.GetActiveScene().path != error.ScenePath)
            {
                if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(error.ScenePath);
                }
                else
                {
                    return;
                }
            }
            
            var targetObject = GameObject.Find(error.HierarchyPath);
            if (targetObject != null)
            {
                var targetComponent = targetObject.GetComponent(error.ComponentTypeName);
                if (targetComponent != null)
                {
                    Selection.activeObject = targetComponent;
                    EditorGUIUtility.PingObject(targetComponent);
                }
                else
                {
                    Debug.LogWarning($"컴포넌트를 찾을 수 없습니다: {error.ComponentTypeName}");
                }
            }
            else
            {
                Debug.LogWarning($"게임오브젝트를 찾을 수 없습니다: {error.HierarchyPath}");
            }
        }
    }
}