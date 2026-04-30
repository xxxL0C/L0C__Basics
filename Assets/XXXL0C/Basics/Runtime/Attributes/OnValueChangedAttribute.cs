using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class OnValueChangedAttribute : PropertyAttribute
    {
        public string MethodName { get; }
        public OnValueChangedAttribute(string methodName) => MethodName = methodName;
    }
}
