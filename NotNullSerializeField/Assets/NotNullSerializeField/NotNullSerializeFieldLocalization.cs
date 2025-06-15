using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JinStudio.NotNull
{
    public enum DisplayLanguage
    {
        Default, // System Language
        English,
        Japanese,
        Korean
    }

    public class NotNullLocalization
    {
        public enum StringKey
        {
            // --- For NotNull Validator Window ---
            Validator_WindowTitle,
            Validator_HelpBox_Description,
            Validator_Button_RunValidation,
            Validator_Button_GoToError,
            Validator_Log_ValidationSuccess,
            Validator_Log_ValidationFailed,
            Validator_Result_AwaitingValidation,
            Validator_Result_Success,
            Validator_Result_ErrorsFound,
            Validator_Error_MisuseWarning,
            Error_NotNullViolation,
            Error_NotNullViolation_WithHierarchy,
            Validator_Dropdown_Scope,
            Scope_CurrentOpen,
            Scope_InBuild,
            Scope_AllProject,
            Validator_Warning_NoScenesToValidate,
            Validator_Toggle_EnableBlocker,

            // --- For Progress Bar ---
            ProgressBar_Title,
            ProgressBar_Info,

            // --- For Drawer & Inspector ---
            Drawer_Tooltip_NotNull,
            Drawer_Error_SerializeField_Formatted,

            // --- For PlayMode Blocker ---
            Blocker_Log_ValidationFailed,
        }

        private static readonly Dictionary<StringKey, string> English = new()
        {
            // Validator
            { StringKey.Validator_WindowTitle, "NotNull Validator" },
            { StringKey.Validator_HelpBox_Description, "Select the validation scope and click the button to scan for any unassigned [NotNull] fields." },
            { StringKey.Validator_Button_RunValidation, "Run Validation" },
            { StringKey.Validator_Button_GoToError, "Go To" },
            { StringKey.Validator_Log_ValidationSuccess, "✅ [NotNull] Validation Successful! All required fields are assigned." },
            { StringKey.Validator_Log_ValidationFailed, "❌ [NotNull] Validation Failed: Found {0} error(s)." },
            { StringKey.Validator_Result_AwaitingValidation, "Click the button above to start validation." },
            { StringKey.Validator_Result_Success, "✅ Validation Complete: All fields are valid." },
            { StringKey.Validator_Result_ErrorsFound, "❌ Validation Complete: Found {0} error(s)." },
            { StringKey.Validator_Error_MisuseWarning, "[NotNull Misuse] The field '{0}' on component '{1}' has [NotNull] but is not a Unity Object type." },
            { StringKey.Error_NotNullViolation, "[NotNull Violation] The field '{0}' on component '{1}' is empty." },
            { StringKey.Error_NotNullViolation_WithHierarchy, "[NotNull Violation] In {0} -> {1} -> {2}, field '{3}' is empty." },
            { StringKey.Validator_Dropdown_Scope, "Validation Scope" },
            { StringKey.Scope_CurrentOpen, "Current Open Scenes" },
            { StringKey.Scope_InBuild, "Scenes in Build Settings" },
            { StringKey.Scope_AllProject, "All Project Scenes" },
            { StringKey.Validator_Warning_NoScenesToValidate, "[NotNull] Skipping validation: No scenes to validate." },
            { StringKey.Validator_Toggle_EnableBlocker, "Enable Play Mode Blocker" },

            // Progress Bar
            { StringKey.ProgressBar_Title, "[NotNull] Validating All Scenes" },
            { StringKey.ProgressBar_Info, "Validating Scene: {0}" },

            // Drawer & Inspector
            { StringKey.Drawer_Tooltip_NotNull, "The field '{0}' cannot be empty. Please assign a value." },
            { StringKey.Drawer_Error_SerializeField_Formatted, "[NotNull] on field '{0}' requires the [SerializeField] attribute because it is private." },

            // Blocker
            { StringKey.Blocker_Log_ValidationFailed, "❌ [NotNull] Validation Failed: Found {0} error(s). Cancelling Play Mode entry." },
        };

        private static readonly Dictionary<StringKey, string> Korean = new()
        {
            // Validator
            { StringKey.Validator_WindowTitle, "NotNull 검증기" },
            { StringKey.Validator_HelpBox_Description, "검증 범위를 선택하고 아래 버튼을 클릭하여 [NotNull] 필드가 비어있는지 검사합니다." },
            { StringKey.Validator_Button_RunValidation, "검증 실행" },
            { StringKey.Validator_Button_GoToError, "추적" },
            { StringKey.Validator_Log_ValidationSuccess, "✅ [NotNull] 검증 성공! 모든 필수 필드가 할당되었습니다." },
            { StringKey.Validator_Log_ValidationFailed, "❌ [NotNull] 검증 실패: {0}개의 오류가 발견되었습니다." },
            { StringKey.Validator_Result_AwaitingValidation, "검증을 실행해주세요." },
            { StringKey.Validator_Result_Success, "✅ 검증 완료: 모든 필드가 유효합니다." },
            { StringKey.Validator_Result_ErrorsFound, "❌ 검증 완료: {0}개의 오류가 발견되었습니다." },
            { StringKey.Validator_Error_MisuseWarning, "[NotNull 오용] '{1}' 컴포넌트의 '{0}' 필드는 [NotNull]이지만 Unity Object 타입이 아닙니다." },
            { StringKey.Error_NotNullViolation, "[NotNull 위반] '{1}' 컴포넌트의 '{0}' 필드가 비어있습니다." },
            { StringKey.Error_NotNullViolation_WithHierarchy, "[NotNull 위반] {0} -> {1} -> {2}의 '{3}' 필드가 비어있습니다." },
            { StringKey.Validator_Dropdown_Scope, "검증 범위" },
            { StringKey.Scope_CurrentOpen, "현재 열린 씬" },
            { StringKey.Scope_InBuild, "빌드 설정의 씬" },
            { StringKey.Scope_AllProject, "프로젝트의 모든 씬" },
            { StringKey.Validator_Warning_NoScenesToValidate, "[NotNull] 검증을 건너뜁니다: 검사할 씬이 없습니다." },
            { StringKey.Validator_Toggle_EnableBlocker, "플레이 모드 진입 방지 활성화" },
            
            // Progress Bar
            { StringKey.ProgressBar_Title, "[NotNull] 전체 검증 중" },
            { StringKey.ProgressBar_Info, "씬 검사 중: {0}" },

            // Drawer & Inspector
            { StringKey.Drawer_Tooltip_NotNull, "'{0}' 필드는 비워둘 수 없습니다. 값을 할당해주세요." },
            { StringKey.Drawer_Error_SerializeField_Formatted, "필드 '{0}'는 [NotNull] 속성이 있지만 private이므로 [SerializeField] 속성도 필요합니다." },

            // Blocker
            { StringKey.Blocker_Log_ValidationFailed, "❌ [NotNull] 검증 실패: {0}개의 오류를 발견했습니다. 플레이 모드 진입을 취소합니다." },
        };

        private static readonly Dictionary<StringKey, string> Japanese = new()
        {
            // Validator
            { StringKey.Validator_WindowTitle, "NotNull 検証" },
            { StringKey.Validator_HelpBox_Description, "検証範囲を選択し、下のボタンをクリックして [NotNull] フィールドが空でないか検査します。" },
            { StringKey.Validator_Button_RunValidation, "検証を実行" },
            { StringKey.Validator_Button_GoToError, "追跡" },
            { StringKey.Validator_Log_ValidationSuccess, "✅ [NotNull] 検証成功！全ての必須フィールドが割り当てられています。" },
            { StringKey.Validator_Log_ValidationFailed, "❌ [NotNull] 検証失敗：{0}個のエラーが発見されました。" },
            { StringKey.Validator_Result_AwaitingValidation, "上のボタンをクリックして検証を開始してください。" },
            { StringKey.Validator_Result_Success, "✅ 検証完了：全てのフィールドは有効です。" },
            { StringKey.Validator_Result_ErrorsFound, "❌ 検証完了：{0}個のエラーが発見されました。" },
            { StringKey.Validator_Error_MisuseWarning, "[NotNull の誤用] コンポーネント '{1}' のフィールド '{0}' は [NotNull] 属性が付いていますが、Unity Object 型ではありません。" },
            { StringKey.Error_NotNullViolation, "[NotNull 違反] コンポーネント '{1}' のフィールド '{0}' が null です。" },
            { StringKey.Error_NotNullViolation_WithHierarchy, "[NotNull 違反] {0} -> {1} -> {2} のフィールド '{3}' が null です。" },
            { StringKey.Validator_Dropdown_Scope, "検証範囲" },
            { StringKey.Scope_CurrentOpen, "現在開いているシーン" },
            { StringKey.Scope_InBuild, "ビルド設定のシーン" },
            { StringKey.Scope_AllProject, "プロジェクトの全シーン" },
            { StringKey.Validator_Warning_NoScenesToValidate, "[NotNull] 検証をスキップします：検査するシーンがありません。" },
            { StringKey.Validator_Toggle_EnableBlocker, "プレイモード移行防止を有効にする" },
            
            // Progress Bar
            { StringKey.ProgressBar_Title, "[NotNull] 全シーンを検証中" },
            { StringKey.ProgressBar_Info, "シーンを検証中：{0}" },
            
            // Drawer & Inspector
            { StringKey.Drawer_Tooltip_NotNull, "フィールド '{0}' は空にできません。値を割り当ててください。" },
            { StringKey.Drawer_Error_SerializeField_Formatted, "フィールド '{0}' には [NotNull] 属性がありますが、private のため [SerializeField] 属性も必要です。" },
            
            // Blocker
            { StringKey.Blocker_Log_ValidationFailed, "❌ [NotNull] 検証失敗：{0}個のエラーが発見されました。プレイモードへの移行をキャンセルします。" },
        };

        public static string Get(StringKey key)
        {
            var selectedLanguage = (DisplayLanguage)EditorPrefs.GetInt("NotNullValidator_Language", (int)DisplayLanguage.Default);
            SystemLanguage targetLanguage;
            switch (selectedLanguage)
            {
                case DisplayLanguage.English:
                    targetLanguage = SystemLanguage.English;
                    break;
                case DisplayLanguage.Japanese:
                    targetLanguage = SystemLanguage.Japanese;
                    break;
                case DisplayLanguage.Korean:
                    targetLanguage = SystemLanguage.Korean;
                    break;
                case DisplayLanguage.Default:
                default:
                    targetLanguage = Application.systemLanguage;
                    break;
            }

            switch (targetLanguage)
            {
                case SystemLanguage.Korean:
                    return Korean.ContainsKey(key) ? Korean[key] : (English.ContainsKey(key) ? English[key] : $"Key not found: {key}");
                case SystemLanguage.Japanese:
                    return Japanese.ContainsKey(key) ? Japanese[key] : (English.ContainsKey(key) ? English[key] : $"Key not found: {key}");
                default:
                    return English.ContainsKey(key) ? English[key] : $"Key not found: {key}";
            }
        }
    }
}