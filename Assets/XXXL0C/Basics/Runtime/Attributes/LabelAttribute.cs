using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class LabelAttribute : PropertyAttribute
    {
        public string DisplayName { get; }
        public LabelAttribute(string displayName) => DisplayName = displayName;
    }
}
