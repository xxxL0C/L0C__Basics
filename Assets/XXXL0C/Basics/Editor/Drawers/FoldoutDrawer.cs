using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.Basics.Editor
{
    [CustomPropertyDrawer(typeof(FoldoutAttribute))]
    public sealed class FoldoutDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr = (FoldoutAttribute)attribute;
            string key = GetPrefsKey(property, attr);
            bool expanded = EditorPrefs.GetBool(key, attr.DefaultExpanded);

            var foldout = new Foldout { text = attr.GroupName, value = expanded };
            foldout.RegisterValueChangedCallback(evt => EditorPrefs.SetBool(key, evt.newValue));

            var field = new UnityEditor.UIElements.PropertyField(property, property.displayName);
            foldout.Add(field);
            return foldout;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (FoldoutAttribute)attribute;
            string key = GetPrefsKey(property, attr);
            bool expanded = EditorPrefs.GetBool(key, attr.DefaultExpanded);

            float lineH = EditorGUIUtility.singleLineHeight;
            Rect headerRect = new Rect(position.x, position.y, position.width, lineH);

            bool newExpanded = EditorGUI.Foldout(headerRect, expanded, attr.GroupName, true);
            if (newExpanded != expanded)
                EditorPrefs.SetBool(key, newExpanded);

            if (newExpanded)
            {
                float spacing = EditorGUIUtility.standardVerticalSpacing;
                Rect fieldRect = new Rect(position.x, position.y + lineH + spacing,
                                           position.width, position.height - lineH - spacing);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(fieldRect, property, label, true);
                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = (FoldoutAttribute)attribute;
            string key = GetPrefsKey(property, attr);
            bool expanded = EditorPrefs.GetBool(key, attr.DefaultExpanded);

            float lineH = EditorGUIUtility.singleLineHeight;
            if (!expanded) return lineH;
            return lineH + EditorGUIUtility.standardVerticalSpacing
                         + EditorGUI.GetPropertyHeight(property, label, true);
        }

        private static string GetPrefsKey(SerializedProperty property, FoldoutAttribute attr)
        {
            int id = property.serializedObject.targetObject.GetInstanceID();
            return $"XXXL0C_Foldout_{id}_{attr.GroupName}";
        }
    }
}
