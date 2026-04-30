using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class PrefixAttribute : PropertyAttribute
    {
        public string Text { get; }
        public PrefixAttribute(string text) => Text = text;
    }
}
