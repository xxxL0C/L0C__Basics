using System;
using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class TypeFilterAttribute : PropertyAttribute
    {
        public Type BaseType { get; }
        public TypeFilterAttribute(Type baseType) => BaseType = baseType;
    }
}
