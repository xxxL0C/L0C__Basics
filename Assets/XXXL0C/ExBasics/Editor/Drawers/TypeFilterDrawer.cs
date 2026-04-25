using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XXXL0C.ExBasics.Editor
{
    [CustomPropertyDrawer(typeof(TypeFilterAttribute))]
    public sealed class TypeFilterDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var attr  = (TypeFilterAttribute)attribute;
            var types = GetConcreteTypes(attr);

            var names = new string[types.Length + 1];
            names[0] = "(None)";
            for (int i = 0; i < types.Length; i++)
                names[i + 1] = types[i].Name;

            int currentIdx = GetCurrentIndex(property, types);
            var dropdown   = new DropdownField(property.displayName, new List<string>(names), currentIdx);
            var nested     = new UnityEditor.UIElements.PropertyField(property, string.Empty);

            var container = new VisualElement();
            container.Add(dropdown);
            container.Add(nested);

            dropdown.RegisterValueChangedCallback(evt =>
            {
                int idx = System.Array.IndexOf(names, evt.newValue);
                property.managedReferenceValue = idx <= 0 ? null : Activator.CreateInstance(types[idx - 1]);
                property.serializedObject.ApplyModifiedProperties();
                nested.MarkDirtyRepaint();
            });

            return container;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var    attr  = (TypeFilterAttribute)attribute;
            var    types = GetConcreteTypes(attr);
            string[] names = BuildNames(types);

            int    currentIdx = GetCurrentIndex(property, types);
            float  lineH      = EditorGUIUtility.singleLineHeight;
            float  spacing    = EditorGUIUtility.standardVerticalSpacing;
            Rect   popRect    = new Rect(position.x, position.y, position.width, lineH);

            EditorGUI.BeginChangeCheck();
            int newIdx = EditorGUI.Popup(popRect, label.text, currentIdx, names);
            if (EditorGUI.EndChangeCheck())
            {
                property.managedReferenceValue = newIdx == 0 ? null : Activator.CreateInstance(types[newIdx - 1]);
            }

            if (property.managedReferenceValue != null)
            {
                Rect childRect = new Rect(position.x, position.y + lineH + spacing,
                                          position.width, position.height - lineH - spacing);
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(childRect, property, GUIContent.none, true);
                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float h = EditorGUIUtility.singleLineHeight;
            if (property.managedReferenceValue != null)
                h += EditorGUIUtility.standardVerticalSpacing
                   + EditorGUI.GetPropertyHeight(property, GUIContent.none, true);
            return h;
        }

        private static Type[] GetConcreteTypes(TypeFilterAttribute attr)
            => TypeCache.GetTypesDerivedFrom(attr.BaseType)
                        .Where(t => !t.IsAbstract && !t.IsInterface)
                        .ToArray();

        private static string[] BuildNames(Type[] types)
        {
            string[] names = new string[types.Length + 1];
            names[0] = "(None)";
            for (int i = 0; i < types.Length; i++)
                names[i + 1] = types[i].Name;
            return names;
        }

        private static int GetCurrentIndex(SerializedProperty property, Type[] types)
        {
            Type current = property.managedReferenceValue?.GetType();
            if (current == null) return 0;
            for (int i = 0; i < types.Length; i++)
                if (types[i] == current) return i + 1;
            return 0;
        }
    }
}
