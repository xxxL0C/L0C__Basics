using System;
using UnityEngine;

namespace XXXL0C.Basics
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField] private bool _hasValue;
        [SerializeField] private T _value;

        public bool HasValue => _hasValue;

        public T Value
        {
            get
            {
                if (!_hasValue)
                    throw new InvalidOperationException("Optional<T> has no value.");
                return _value;
            }
        }

        public static Optional<T> Some(T value) => new Optional<T> { _hasValue = true, _value = value };
        public static Optional<T> None() => new Optional<T> { _hasValue = false };

        public T GetValueOrDefault(T fallback = default) => _hasValue ? _value : fallback;
    }
}
