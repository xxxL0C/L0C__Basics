using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionMember { get; }
        public ShowIfAttribute(string conditionMember) => ConditionMember = conditionMember;
    }
}
