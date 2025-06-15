using System.Collections.Generic;

namespace JinStudio.NotNull
{
    public class ValidationError
    {
        public NotNullLocalization.StringKey MessageKey { get; }
        
        public string ScenePath { get; }
        public string HierarchyPath { get; }
        public string ComponentTypeName { get; }
        public object[] Parameters { get; }

        public ValidationError(NotNullLocalization.StringKey messageKey, 
            string scenePath, string hierarchyPath, string componentTypeName, 
            params object[] parameters)
        {
            MessageKey = messageKey;
            ScenePath = scenePath;
            HierarchyPath = hierarchyPath;
            ComponentTypeName = componentTypeName;
            Parameters = parameters;
        }
    }
    
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<ValidationError> Errors { get; } = new List<ValidationError>();
        public bool IsCancelled { get; set; } = false;
    }
}
