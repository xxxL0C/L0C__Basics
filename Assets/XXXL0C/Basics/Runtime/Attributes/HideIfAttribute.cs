using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class HideIfAttribute : PropertyAttribute
    {
        public string ConditionMember { get; }
        public HideIfAttribute(string conditionMember) => ConditionMember = conditionMember;
    }
}
