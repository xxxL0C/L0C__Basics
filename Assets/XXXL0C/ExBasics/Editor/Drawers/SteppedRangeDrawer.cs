using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(SteppedRangeAttribute))]
    public sealed class SteppedRangeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (SteppedRangeAttribute)attribute;
            var container = new VisualElement();

            var slider = new Slider(property.displayName, attr.Min, attr.Max)
            {
                value = property.floatValue
            };
            slider.RegisterValueChangedCallback(evt =>
            {
                float snapped = Snap(evt.newValue, attr.Step, attr.Min);
                slider.SetValueWithoutNotify(snapped);
                property.floatValue = snapped;
                property.serializedObject.ApplyModifiedProperties();
            });
            container.Add(slider);
            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (SteppedRangeAttribute)attribute;
            EditorGUI.BeginChangeCheck();
            float raw = EditorGUI.Slider(position, label, property.floatValue, attr.Min, attr.Max);
            if (EditorGUI.EndChangeCheck())
                property.floatValue = Snap(raw, attr.Step, attr.Min);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight;

        private static float Snap(float value, float step, float min)
            => min + Mathf.Round((value - min) / step) * step;
    }
}
