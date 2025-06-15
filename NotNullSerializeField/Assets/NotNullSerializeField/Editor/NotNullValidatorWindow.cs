using UnityEngine;
using UnityEditor;

namespace JinStudio.NotNull
{
    public enum ValidationScope
    {
        CurrentOpenScenes,
        ScenesInBuildSettings,
        AllProjectScenes
    }

    public class NotNullValidatorWindow : EditorWindow
    {
        private NotNullWindowLogic _logic;
        private ValidationResult _lastResult;
        private Vector2 _scrollPosition;
        
        private string[] _scopeDisplayOptions;
        private string[] _languageDisplayOptions;

        [MenuItem("Tools/JinStudio/NotNull Attribute Setting")]
        public static void ShowWindow()
        {
            var window = GetWindow<NotNullValidatorWindow>();
            window.minSize = new Vector2(450, 300);
            window.Show();
        }

        public void DisplayValidationResult(ValidationResult result)
        {
            _lastResult = result;
            Repaint();
        }

        private void OnEnable()
        {
            _logic = new NotNullWindowLogic(this);
            _languageDisplayOptions = new string[] { "Default (System Language)", "English", "Japanese", "Korean" };
            ReloadDisplayOptions();
        }

        private void ReloadDisplayOptions()
        {
            titleContent = new GUIContent(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_WindowTitle));
            _scopeDisplayOptions = new string[]
            {
                NotNullLocalization.Get(NotNullLocalization.StringKey.Scope_CurrentOpen),
                NotNullLocalization.Get(NotNullLocalization.StringKey.Scope_InBuild),
                NotNullLocalization.Get(NotNullLocalization.StringKey.Scope_AllProject)
            };
        }

        private void OnGUI()
        {
            DrawHeader();
            DrawSettings();
            DrawValidationButton();
            DrawResults();
        }

        private void DrawHeader()
        {
            EditorGUILayout.LabelField(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_WindowTitle), EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_HelpBox_Description), MessageType.Info);
            EditorGUILayout.Space();
        }

        private void DrawSettings()
        {
            DrawLanguageSetting();
            DrawScopeSetting();
            DrawBlockerSetting();
            EditorGUILayout.Space();
        }

        private void DrawLanguageSetting()
        {
            EditorGUI.BeginChangeCheck();
            var newLangIndex = EditorGUILayout.Popup("언어 설정 (Language)", (int)NotNullValidatorSettings.Language, _languageDisplayOptions);
            if (EditorGUI.EndChangeCheck())
            {
                NotNullValidatorSettings.SaveLanguage((DisplayLanguage)newLangIndex);
                ReloadDisplayOptions();
            }
        }

        private void DrawScopeSetting()
        {
            EditorGUI.BeginChangeCheck();
            var scopeLabel = NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Dropdown_Scope);
            var newScopeIndex = EditorGUILayout.Popup(scopeLabel, (int)NotNullValidatorSettings.Scope, _scopeDisplayOptions);
            if (EditorGUI.EndChangeCheck())
            {
                NotNullValidatorSettings.SaveScope((ValidationScope)newScopeIndex);
            }
        }

        private void DrawBlockerSetting()
        {
            EditorGUILayout.BeginHorizontal();
            var blockerLabel = NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Toggle_EnableBlocker);
            EditorGUILayout.LabelField(blockerLabel, EditorStyles.wordWrappedLabel);
            var newBlockerState = EditorGUILayout.Toggle(NotNullValidatorSettings.IsBlockerEnabled, GUILayout.Width(20));
            EditorGUILayout.EndHorizontal();

            if (newBlockerState != NotNullValidatorSettings.IsBlockerEnabled)
            {
                NotNullValidatorSettings.SaveBlockerEnabled(newBlockerState);
            }
        }

        private void DrawValidationButton()
        {
            if (GUILayout.Button(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Button_RunValidation), GUILayout.Height(40)))
            {
                _logic.ExecuteValidation();
            }
            EditorGUILayout.Space();
        }

        private void DrawResults()
        {
            if (_lastResult == null)
            {
                EditorGUILayout.LabelField(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Result_AwaitingValidation), EditorStyles.centeredGreyMiniLabel);
                return;
            }
            if (_lastResult.IsCancelled) return;
            if (_lastResult.IsValid)
            {
                GUI.color = Color.green;
                EditorGUILayout.LabelField(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Result_Success), EditorStyles.boldLabel);
                GUI.color = Color.white;
            }
            else
            {
                GUI.color = Color.yellow;
                EditorGUILayout.LabelField(string.Format(NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Result_ErrorsFound),_lastResult.Errors.Count), EditorStyles.boldLabel);
                GUI.color = Color.white;
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.MaxHeight(400));
                EditorGUILayout.Separator();
                var goToButtonText = NotNullLocalization.Get(NotNullLocalization.StringKey.Validator_Button_GoToError);
                foreach (var error in _lastResult.Errors)
                {
                    EditorGUILayout.BeginHorizontal();
                    var message = string.Format(NotNullLocalization.Get(error.MessageKey), error.Parameters);
                    EditorGUILayout.LabelField(message, EditorStyles.wordWrappedLabel);
                    if (GUILayout.Button(goToButtonText, GUILayout.Width(60)))
                    {
                        _logic.GoToErrorObject(error);
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(4);
                }
                EditorGUILayout.EndScrollView();
            }
        }
    }
}