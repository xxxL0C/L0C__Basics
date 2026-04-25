using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class PrefixAttribute : PropertyAttribute
    {
        public string Text { get; }
        public PrefixAttribute(string text) => Text = text;
    }
}
