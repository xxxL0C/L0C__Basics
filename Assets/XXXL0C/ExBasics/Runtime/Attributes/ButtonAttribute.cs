using System;

namespace XXXL0C.ExBasics
{
    public enum ButtonMode { Always, EditorOnly, PlayModeOnly }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ButtonAttribute : Attribute
    {
        public string     Label { get; }
        public ButtonMode Mode  { get; }

        public ButtonAttribute(string label = null, ButtonMode mode = ButtonMode.Always)
        {
            Label = label;
            Mode  = mode;
        }
    }
}
