using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class HideIfAttribute : PropertyAttribute
    {
        public string ConditionMember { get; }
        public HideIfAttribute(string conditionMember) => ConditionMember = conditionMember;
    }
}
