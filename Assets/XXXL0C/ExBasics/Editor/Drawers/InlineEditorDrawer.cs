using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(InlineEditorAttribute))]
    public sealed class InlineEditorDrawer : PropertyDrawer
    {
        private UnityEditor.Editor _cachedEditor;
        private Object             _cachedTarget;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr      = (InlineEditorAttribute)attribute;
            var container = new VisualElement();
            var objField  = new PropertyField(property);
            container.Add(objField);

            InspectorElement inlineInsp = null;

            void Rebuild()
            {
                if (inlineInsp != null)
                {
                    container.Remove(inlineInsp);
                    inlineInsp = null;
                }
                var target = property.objectReferenceValue;
                if (target == null) return;
                inlineInsp = new InspectorElement(target);
                container.Add(inlineInsp);
            }

            objField.RegisterValueChangeCallback(_ => Rebuild());
            Rebuild();
            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var   attr     = (InlineEditorAttribute)attribute;
            float lineH    = EditorGUIUtility.singleLineHeight;
            float spacing  = EditorGUIUtility.standardVerticalSpacing;

            Rect fieldRect = new Rect(position.x, position.y, position.width, lineH);
            EditorGUI.PropertyField(fieldRect, property, label, false);

            var target = property.objectReferenceValue;

            if (attr.ShowOpenButton && target != null)
            {
                const float btnW = 50f;
                Rect btnRect = new Rect(position.xMax - btnW, position.y, btnW, lineH);
                if (GUI.Button(btnRect, "Open"))
                    AssetDatabase.OpenAsset(target);
            }

            if (target == null) return;

            GetEditor(target);
            if (_cachedEditor == null) return;

            Rect areaRect = new Rect(position.x, position.y + lineH + spacing,
                                     position.width, position.height - lineH - spacing);

            GUILayout.BeginArea(areaRect);
            _cachedEditor.OnInspectorGUI();
            GUILayout.EndArea();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float h = EditorGUIUtility.singleLineHeight;
            if (property.objectReferenceValue != null)
                h += EditorGUIUtility.standardVerticalSpacing + 200f;
            return h;
        }

        private void GetEditor(Object target)
        {
            if (_cachedTarget == target) return;
            if (_cachedEditor != null)
                Object.DestroyImmediate(_cachedEditor);
            _cachedEditor = target != null ? UnityEditor.Editor.CreateEditor(target) : null;
            _cachedTarget = target;
        }
    }
}
