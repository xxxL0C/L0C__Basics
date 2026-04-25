using System;
using UnityEngine;

namespace XXXL0C.ExBasics
{
    [Serializable]
    public struct FloatRange
    {
        [SerializeField] public float min;
        [SerializeField] public float max;

        public FloatRange(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
