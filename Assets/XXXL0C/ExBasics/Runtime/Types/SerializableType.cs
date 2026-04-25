using System;
using UnityEngine;

namespace XXXL0C.ExBasics
{
    [Serializable]
    public sealed class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] private string _assemblyQualifiedName;

        private Type _type;

        public Type Type
        {
            get => _type;
            set
            {
                _type = value;
                _assemblyQualifiedName = value?.AssemblyQualifiedName;
            }
        }

        public SerializableType() { }

        public SerializableType(Type type)
        {
            Type = type;
        }

        public void OnBeforeSerialize()
        {
            _assemblyQualifiedName = _type?.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            _type = string.IsNullOrEmpty(_assemblyQualifiedName)
                ? null
                : Type.GetType(_assemblyQualifiedName);
        }
    }
}
