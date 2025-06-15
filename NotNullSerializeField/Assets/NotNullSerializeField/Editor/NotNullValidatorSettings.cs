using UnityEngine;
using UnityEditor;

namespace JinStudio.NotNull
{
    /// <summary>
    /// NotNull 도구의 모든 설정을 관리하고 EditorPrefs에 저장/로드하는 정적 클래스입니다.
    /// </summary>
    public static class NotNullValidatorSettings
    {
        private const string PREFS_KEY_SCOPE = "NotNullValidator_ValidationScope";
        private const string PREFS_KEY_LANGUAGE = "NotNullValidator_Language";
        private const string PREFS_KEY_BLOCKER_ENABLED = "NotNullValidator_BlockerEnabled";

        public static ValidationScope Scope { get; private set; }
        public static DisplayLanguage Language { get; private set; }
        public static bool IsBlockerEnabled { get; private set; }

        // 클래스가 처음 사용될 때 저장된 설정을 불러옵니다.
        static NotNullValidatorSettings()
        {
            Load();
        }

        public static void Load()
        {
            Scope = (ValidationScope)EditorPrefs.GetInt(PREFS_KEY_SCOPE, (int)ValidationScope.AllProjectScenes);
            Language = (DisplayLanguage)EditorPrefs.GetInt(PREFS_KEY_LANGUAGE, (int)DisplayLanguage.Default);
            IsBlockerEnabled = EditorPrefs.GetBool(PREFS_KEY_BLOCKER_ENABLED, true);
        }

        public static void SaveScope(ValidationScope newScope)
        {
            Scope = newScope;
            EditorPrefs.SetInt(PREFS_KEY_SCOPE, (int)Scope);
        }

        public static void SaveLanguage(DisplayLanguage newLanguage)
        {
            Language = newLanguage;
            EditorPrefs.SetInt(PREFS_KEY_LANGUAGE, (int)Language);
        }

        public static void SaveBlockerEnabled(bool isEnabled)
        {
            IsBlockerEnabled = isEnabled;
            EditorPrefs.SetBool(PREFS_KEY_BLOCKER_ENABLED, isEnabled);
        }
    }
}