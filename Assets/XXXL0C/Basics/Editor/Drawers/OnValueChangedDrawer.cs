using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.Basics.Editor
{
    [CustomPropertyDrawer(typeof(OnValueChangedAttribute))]
    public sealed class OnValueChangedDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (OnValueChangedAttribute)attribute;
            var field = new PropertyField(property);
            field.RegisterValueChangeCallback(_ =>
            {
                property.serializedObject.ApplyModifiedProperties();
                object owner = ReflectionUtility.GetPropertyOwner(property);
                ReflectionUtility.InvokeMethod(owner, attr.MethodName);
            });
            return field;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label, true);
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
                object owner = ReflectionUtility.GetPropertyOwner(property);
                var attr = (OnValueChangedAttribute)attribute;
                ReflectionUtility.InvokeMethod(owner, attr.MethodName);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);
    }
}
