using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionMember { get; }
        public ShowIfAttribute(string conditionMember) => ConditionMember = conditionMember;
    }
}
