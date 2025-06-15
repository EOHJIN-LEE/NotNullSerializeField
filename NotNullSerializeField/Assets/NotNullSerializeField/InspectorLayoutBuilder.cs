using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace JinStudio.NotNull
{
    /// <summary>
    /// 스크립트의 필드를 분석하여 그릴 수 있는 정보(DrawableField)의 목록을 생성하는 책임을 가집니다.
    /// </summary>
    public class InspectorLayoutBuilder
    {
        public List<DrawableField> GetDrawableFields(Object target, SerializedObject serializedObject)
        {
            if (target == null) return new List<DrawableField>();

            var drawableFields = new List<DrawableField>();
            var allFields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in allFields)
            {
                if (field.Name == "m_Script") continue;

                var drawableField = new DrawableField
                {
                    FieldInfo = field,
                    Property = serializedObject.FindProperty(field.Name)
                };

                var notNullAttr = field.GetCustomAttribute<NotNull>(false);
                if (notNullAttr != null)
                {
                    drawableField.NotNullInfo = new NotNullFieldInfo
                    {
                        NotNullAttr = notNullAttr,
                        IsUnityObjectReference = typeof(Object).IsAssignableFrom(field.FieldType)
                    };
                }

                drawableFields.Add(drawableField);
            }
            return drawableFields;
        }
    }
}