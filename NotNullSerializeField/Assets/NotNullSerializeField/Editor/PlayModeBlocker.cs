using UnityEngine;
using UnityEditor;

namespace JinStudio.NotNull
{
    [InitializeOnLoad]
    public class PlayModeBlocker
    {
        private static readonly NotNullFieldValidator Validator;

        static PlayModeBlocker()
        {
            Validator = new NotNullFieldValidator();
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!NotNullValidatorSettings.IsBlockerEnabled)
            {
                return;
            }

            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var scope = NotNullValidatorSettings.Scope;
                ValidationResult result = null;

                switch (scope)
                {
                    case ValidationScope.CurrentOpenScenes:
                        result = Validator.ValidateAllOpenScenes();
                        break;
                    case ValidationScope.ScenesInBuildSettings:
                        result = Validator.ValidateAllScenesInBuildSettings();
                        break;
                    case ValidationScope.AllProjectScenes:
                        result = Validator.ValidateAllProjectScenes();
                        break;
                }

                if (result == null || result.IsCancelled)
                {
                    if(result != null && result.IsCancelled) EditorApplication.isPlaying = false;
                    return;
                }
                
                if (!result.IsValid)
                {
                    Debug.LogError(string.Format(
                        NotNullLocalization.Get(NotNullLocalization.StringKey.Blocker_Log_ValidationFailed),
                        result.Errors.Count
                    ));
                    
                    var window = EditorWindow.GetWindow<NotNullValidatorWindow>();
                    window.DisplayValidationResult(result);

                    EditorApplication.isPlaying = false;
                }
            }
        }
    }
}