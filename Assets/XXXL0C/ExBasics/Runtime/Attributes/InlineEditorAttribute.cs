using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class InlineEditorAttribute : PropertyAttribute
    {
        public bool ShowOpenButton { get; }
        public InlineEditorAttribute(bool showOpenButton = true) => ShowOpenButton = showOpenButton;
    }
}
