using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class InlineEditorAttribute : PropertyAttribute
    {
        public bool ShowOpenButton { get; }
        public InlineEditorAttribute(bool showOpenButton = true) => ShowOpenButton = showOpenButton;
    }
}
