using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class LabelAttribute : PropertyAttribute
    {
        public string DisplayName { get; }
        public LabelAttribute(string displayName) => DisplayName = displayName;
    }
}
