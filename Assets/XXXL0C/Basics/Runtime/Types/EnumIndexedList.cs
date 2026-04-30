using System;
using System.Collections.Generic;
using UnityEngine;

namespace XXXL0C.Basics
{
    [Serializable]
    public class EnumIndexedList<TEnum, TValue>
        where TEnum : struct, Enum
    {
        [SerializeField] private List<TValue> _items;

        public EnumIndexedList()
        {
            int count = Enum.GetValues(typeof(TEnum)).Length;
            _items = new List<TValue>(count);
            for (int i = 0; i < count; i++)
                _items.Add(default);
        }

        public TValue this[TEnum key]
        {
            get
            {
                EnsureSize();
                return _items[Convert.ToInt32(key)];
            }
            set
            {
                EnsureSize();
                _items[Convert.ToInt32(key)] = value;
            }
        }

        public int Count => Enum.GetValues(typeof(TEnum)).Length;

        private void EnsureSize()
        {
            int needed = Enum.GetValues(typeof(TEnum)).Length;
            while (_items.Count < needed)
                _items.Add(default);
        }
    }
}
