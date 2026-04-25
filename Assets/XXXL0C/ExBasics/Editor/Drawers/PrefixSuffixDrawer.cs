using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(PrefixAttribute))]
    [CustomPropertyDrawer(typeof(SuffixAttribute))]
    public sealed class PrefixSuffixDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            string text    = GetText();
            bool   isPrefix = attribute is PrefixAttribute;

            var row   = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;

            var label = new Label(text);
            label.style.unityTextAlign = TextAnchor.MiddleCenter;
            label.style.paddingLeft    = 2;
            label.style.paddingRight   = 2;

            var field = new UnityEditor.UIElements.PropertyField(property, string.Empty);
            field.style.flexGrow = 1;

            if (isPrefix)
            {
                row.Add(label);
                row.Add(field);
            }
            else
            {
                row.Add(field);
                row.Add(label);
            }
            return row;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string text     = GetText();
            bool   isPrefix = attribute is PrefixAttribute;
            float  textW    = GUI.skin.label.CalcSize(new GUIContent(text)).x + 4f;

            Rect textRect, fieldRect;
            if (isPrefix)
            {
                textRect  = new Rect(position.x + EditorGUIUtility.labelWidth, position.y, textW, position.height);
                fieldRect = new Rect(textRect.xMax, position.y, position.xMax - textRect.xMax, position.height);
            }
            else
            {
                fieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y,
                                     position.width - EditorGUIUtility.labelWidth - textW, position.height);
                textRect  = new Rect(fieldRect.xMax, position.y, textW, position.height);
            }

            EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), label);
            EditorGUI.LabelField(textRect, text);
            EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        private string GetText() => attribute is PrefixAttribute p ? p.Text : ((SuffixAttribute)attribute).Text;
    }
}
