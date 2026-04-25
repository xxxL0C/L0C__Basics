using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public sealed class HideIfDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var    attr  = (HideIfAttribute)attribute;
            var    field = new PropertyField(property);
            field.schedule.Execute(() =>
            {
                object owner = ReflectionUtility.GetPropertyOwner(property);
                bool   hide  = ReflectionUtility.GetMemberBoolValue(owner, attr.ConditionMember);
                field.style.display = hide ? DisplayStyle.None : DisplayStyle.Flex;
            }).Every(100);
            return field;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ShouldHide(property)) return;
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => ShouldHide(property) ? 0f : EditorGUI.GetPropertyHeight(property, label, true);

        private bool ShouldHide(SerializedProperty property)
        {
            var    attr  = (HideIfAttribute)attribute;
            object owner = ReflectionUtility.GetPropertyOwner(property);
            return ReflectionUtility.GetMemberBoolValue(owner, attr.ConditionMember);
        }
    }
}
