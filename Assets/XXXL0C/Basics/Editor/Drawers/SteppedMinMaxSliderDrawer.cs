using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.Basics.Editor
{
    [CustomPropertyDrawer(typeof(SteppedMinMaxSliderAttribute))]
    public sealed class SteppedMinMaxSliderDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (SteppedMinMaxSliderAttribute)attribute;
            var minProp = property.FindPropertyRelative("min");
            var maxProp = property.FindPropertyRelative("max");

            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var minField = new UnityEngine.UIElements.FloatField { value = minProp.floatValue };
            minField.style.width = 50;

            var slider = new MinMaxSlider(minProp.floatValue, maxProp.floatValue, attr.Min, attr.Max);
            slider.style.flexGrow = 1;

            var maxField = new UnityEngine.UIElements.FloatField { value = maxProp.floatValue };
            maxField.style.width = 50;

            slider.RegisterValueChangedCallback(evt =>
            {
                float lo = Snap(evt.newValue.x, attr.Step, attr.Min);
                float hi = Snap(evt.newValue.y, attr.Step, attr.Min);
                lo = Mathf.Clamp(lo, attr.Min, hi);
                hi = Mathf.Clamp(hi, lo, attr.Max);
                minProp.floatValue = lo;
                maxProp.floatValue = hi;
                property.serializedObject.ApplyModifiedProperties();
                minField.SetValueWithoutNotify(lo);
                maxField.SetValueWithoutNotify(hi);
                slider.SetValueWithoutNotify(new UnityEngine.Vector2(lo, hi));
            });

            container.Add(minField);
            container.Add(slider);
            container.Add(maxField);
            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (SteppedMinMaxSliderAttribute)attribute;
            var minProp = property.FindPropertyRelative("min");
            var maxProp = property.FindPropertyRelative("max");

            if (minProp == null || maxProp == null)
            {
                EditorGUI.HelpBox(position, "[SteppedMinMaxSlider] requires a struct with 'min' and 'max' float fields.", MessageType.Error);
                return;
            }

            float lo = minProp.floatValue;
            float hi = maxProp.floatValue;

            position = EditorGUI.PrefixLabel(position, label);

            const float numW = 45f;
            const float space = 4f;

            Rect loRect = new Rect(position.x, position.y, numW, position.height);
            Rect slRect = new Rect(position.x + numW + space, position.y,
                                   position.width - 2f * (numW + space), position.height);
            Rect hiRect = new Rect(position.xMax - numW, position.y, numW, position.height);

            EditorGUI.BeginChangeCheck();
            lo = EditorGUI.FloatField(loRect, lo);
            EditorGUI.MinMaxSlider(slRect, ref lo, ref hi, attr.Min, attr.Max);
            hi = EditorGUI.FloatField(hiRect, hi);

            if (EditorGUI.EndChangeCheck())
            {
                lo = Snap(lo, attr.Step, attr.Min);
                hi = Snap(hi, attr.Step, attr.Min);
                lo = Mathf.Clamp(lo, attr.Min, hi);
                hi = Mathf.Clamp(hi, lo, attr.Max);
                minProp.floatValue = lo;
                maxProp.floatValue = hi;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight;

        private static float Snap(float value, float step, float min)
            => min + Mathf.Round((value - min) / step) * step;
    }
}
