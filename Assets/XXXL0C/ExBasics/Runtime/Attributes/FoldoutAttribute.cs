using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class FoldoutAttribute : PropertyAttribute
    {
        public string GroupName      { get; }
        public bool   DefaultExpanded { get; }

        public FoldoutAttribute(string groupName, bool defaultExpanded = true)
        {
            GroupName       = groupName;
            DefaultExpanded = defaultExpanded;
        }
    }
}
