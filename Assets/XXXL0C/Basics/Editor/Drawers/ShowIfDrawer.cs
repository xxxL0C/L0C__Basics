using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.Basics.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public sealed class ShowIfDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (ShowIfAttribute)attribute;
            var field = new PropertyField(property);
            field.schedule.Execute(() =>
            {
                object owner = ReflectionUtility.GetPropertyOwner(property);
                bool show = ReflectionUtility.GetMemberBoolValue(owner, attr.ConditionMember);
                field.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
            }).Every(100);
            return field;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!ShouldShow(property)) return;
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => ShouldShow(property) ? EditorGUI.GetPropertyHeight(property, label, true) : 0f;

        private bool ShouldShow(SerializedProperty property)
        {
            var attr = (ShowIfAttribute)attribute;
            object owner = ReflectionUtility.GetPropertyOwner(property);
            return ReflectionUtility.GetMemberBoolValue(owner, attr.ConditionMember);
        }
    }
}
