using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class SuffixAttribute : PropertyAttribute
    {
        public string Text { get; }
        public SuffixAttribute(string text) => Text = text;
    }
}
