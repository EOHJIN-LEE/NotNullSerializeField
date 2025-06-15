using System.Reflection;
using UnityEditor;

namespace JinStudio.NotNull
{
    /// <summary>
    /// Stores additional information about a field decorated with the [NotNull] attribute.
    /// </summary>
    public class NotNullFieldInfo
    {
        public NotNull NotNullAttr;
        public bool IsUnityObjectReference;
    }

    /// <summary>
    /// Consolidates all the analyzed information for a single field that will be rendered in the custom inspector.
    /// </summary>
    public class DrawableField
    {
        public FieldInfo FieldInfo;
        public SerializedProperty Property;
        public NotNullFieldInfo NotNullInfo;
    }
}
