using UnityEngine;

namespace XXXL0C.ExBasics
{
    public sealed class OnValueChangedAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public OnValueChangedAttribute(string methodName) => MethodName = methodName;
    }
}
