using UnityEngine;
using UnityEditor;

namespace JinStudio.NotNull
{
    [CustomPropertyDrawer(typeof(NotNull))]
    public class NotNullDrawer : PropertyDrawer
    {
        private const float HelpBoxHeight = 30f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            var isError = property.objectReferenceValue == null;

            DrawPropertyField(position, property, label, isError);

            if (isError)
            {
                DrawHelpBox(position, property, label);
            }
        }

        private void DrawPropertyField(Rect position, SerializedProperty property, GUIContent label, bool isError)
        {
            var propertyRect = position;
            propertyRect.height = EditorGUI.GetPropertyHeight(property, label);
            
            var originalBackgroundColor = GUI.backgroundColor;
            
            if (isError)
            {
                GUI.backgroundColor = new Color(1f, 0.7f, 0.7f, 1f);
            }
            
            EditorGUI.PropertyField(propertyRect, property, label, true);
            
            GUI.backgroundColor = originalBackgroundColor;
        }
        
        private void DrawHelpBox(Rect position, SerializedProperty property, GUIContent label)
        {
            var propertyHeight = EditorGUI.GetPropertyHeight(property, label);

            var errorMessage = string.Format(
                NotNullLocalization.Get(NotNullLocalization.StringKey.Drawer_Tooltip_NotNull),
                property.displayName
            );

            var helpBoxRect = position;
            helpBoxRect.y += propertyHeight + 2f;
            helpBoxRect.height = HelpBoxHeight;

            EditorGUI.HelpBox(helpBoxRect, errorMessage, MessageType.Error);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalHeight = base.GetPropertyHeight(property, label);
            
            if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
            {
                totalHeight += HelpBoxHeight + 4f;
            }

            return totalHeight;
        }
    }
}