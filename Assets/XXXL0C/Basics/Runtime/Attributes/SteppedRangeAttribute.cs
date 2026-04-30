using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class SteppedRangeAttribute : PropertyAttribute
    {
        public float Min { get; }
        public float Max { get; }
        public float Step { get; }

        public SteppedRangeAttribute(float min, float max, float step)
        {
            Min = min;
            Max = max;
            Step = step;
        }
    }
}
