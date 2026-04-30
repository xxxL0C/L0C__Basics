using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace XXXL0C.Basics.Editor
{
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public sealed class ButtonEditorMono : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ButtonEditorCore.DrawButtons(target);
        }
    }

    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true)]
    public sealed class ButtonEditorSO : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ButtonEditorCore.DrawButtons(target);
        }
    }

    internal static class ButtonEditorCore
    {
        private const BindingFlags MethodFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void DrawButtons(Object target)
        {
            if (target == null) return;
            var type = target.GetType();

            foreach (var method in type.GetMethods(MethodFlags))
            {
                var attr = method.GetCustomAttribute<ButtonAttribute>();
                if (attr == null) continue;
                if (method.GetParameters().Length > 0) continue;

                bool shouldShow = attr.Mode switch
                {
                    ButtonMode.EditorOnly => !Application.isPlaying,
                    ButtonMode.PlayModeOnly => Application.isPlaying,
                    _ => true
                };
                if (!shouldShow) continue;

                string label = string.IsNullOrEmpty(attr.Label)
                    ? ObjectNames.NicifyVariableName(method.Name)
                    : attr.Label;

                if (GUILayout.Button(label))
                {
                    Undo.RecordObject(target, $"Button: {label}");
                    method.Invoke(target, null);
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}
