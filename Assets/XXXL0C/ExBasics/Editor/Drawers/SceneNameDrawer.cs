using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public sealed class SceneNameDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            string[] names = GetSceneNames();
            if (names.Length == 0)
            {
                return new HelpBox("No scenes in Build Settings.", HelpBoxMessageType.Warning);
            }

            int currentIdx = System.Array.IndexOf(names, property.stringValue);
            var dropdown   = new DropdownField(property.displayName, new List<string>(names),
                                               Mathf.Max(0, currentIdx));
            dropdown.RegisterValueChangedCallback(evt =>
            {
                property.stringValue = evt.newValue;
                property.serializedObject.ApplyModifiedProperties();
            });
            return dropdown;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] names = GetSceneNames();
            if (names.Length == 0)
            {
                EditorGUI.HelpBox(position, "No scenes in Build Settings.", MessageType.Warning);
                return;
            }

            int currentIdx = System.Array.IndexOf(names, property.stringValue);
            EditorGUI.BeginChangeCheck();
            int newIdx = EditorGUI.Popup(position, label.text, Mathf.Max(0, currentIdx), names);
            if (EditorGUI.EndChangeCheck())
                property.stringValue = names[newIdx];
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUIUtility.singleLineHeight;

        private static string[] GetSceneNames()
        {
            var scenes = EditorBuildSettings.scenes;
            var names  = new string[scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
                names[i] = Path.GetFileNameWithoutExtension(scenes[i].path);
            return names;
        }
    }
}
