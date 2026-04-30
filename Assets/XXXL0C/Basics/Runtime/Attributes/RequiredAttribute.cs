using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class RequiredAttribute : PropertyAttribute
    {
        public string Message { get; }
        public RequiredAttribute(string message = null) => Message = message;
    }
}
