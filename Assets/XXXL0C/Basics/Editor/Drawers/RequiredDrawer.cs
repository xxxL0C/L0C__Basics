using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.Basics.Editor
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public sealed class RequiredDrawer : PropertyDrawer
    {
        private const float HelpBoxHeight = 38f;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (RequiredAttribute)attribute;
            string msg = attr.Message ?? $"{property.displayName} is required.";
            var container = new VisualElement();
            var field = new PropertyField(property);
            var helpBox = new HelpBox(msg, HelpBoxMessageType.Error);

            container.Add(field);
            container.Add(helpBox);

            UpdateHelpBoxVisibility(property, helpBox);
            field.RegisterValueChangeCallback(_ => UpdateHelpBoxVisibility(property, helpBox));

            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float fieldH = EditorGUI.GetPropertyHeight(property, label, true);
            Rect fieldRect = new Rect(position.x, position.y, position.width, fieldH);
            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (IsMissing(property))
            {
                var attr = (RequiredAttribute)attribute;
                string msg = attr.Message ?? $"{label.text} is required.";
                float spacing = EditorGUIUtility.standardVerticalSpacing;
                Rect helpRect = new Rect(position.x, position.y + fieldH + spacing,
                                          position.width, HelpBoxHeight);
                EditorGUI.HelpBox(helpRect, msg, MessageType.Error);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float h = EditorGUI.GetPropertyHeight(property, label, true);
            if (IsMissing(property))
                h += EditorGUIUtility.standardVerticalSpacing + HelpBoxHeight;
            return h;
        }

        private static bool IsMissing(SerializedProperty property)
            => property.propertyType == SerializedPropertyType.ObjectReference
               && property.objectReferenceValue == null;

        private static void UpdateHelpBoxVisibility(SerializedProperty property, HelpBox helpBox)
        {
            helpBox.style.display = IsMissing(property) ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
