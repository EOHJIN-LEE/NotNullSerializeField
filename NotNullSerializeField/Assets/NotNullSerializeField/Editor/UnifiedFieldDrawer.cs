// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;
//
// namespace JinStudio.NotNull
// {
//     /// <summary>
//     /// 모든 MonoBehaviour를 위한 통합 커스텀 에디터입니다. (최종 조율자 역할)
//     /// ScriptFieldAnalyzer와 FieldDrawer를 생성하고 전체 과정을 조율합니다.
//     /// </summary>
//     [CustomEditor(typeof(MonoBehaviour), true)]
//     [CanEditMultipleObjects]
//     public class UnifiedFieldDrawer : Editor
//     {
//         private InspectorLayoutBuilder _inspectorLayoutBuilder;
//         private FieldDrawer _drawer;
//         private List<DrawableField> _drawableFields;
//
//         private void OnEnable()
//         {
//             // 각 역할을 담당할 클래스의 인스턴스를 생성합니다.
//             _inspectorLayoutBuilder = new InspectorLayoutBuilder();
//             _drawer = new FieldDrawer();
//
//             // 분석가에게 필드 분석을 요청하고, 그 결과를 저장해 둡니다.
//             _drawableFields = _inspectorLayoutBuilder.GetDrawableFields(target, serializedObject);
//         }
//
//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             
//             // 그리기 작업을 FieldDrawer에 위임합니다.
//             _drawer.DrawScriptReferenceField(serializedObject);
//
//             if (_drawableFields != null)
//             {
//                 foreach (var field in _drawableFields)
//                 {
//                     // 그리기 작업을 FieldDrawer에 위임합니다.
//                     _drawer.DrawField(field);
//                 }
//             }
//
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }