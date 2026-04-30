using UnityEngine;

namespace XXXL0C.Basics
{
    public sealed class SteppedMinMaxSliderAttribute : PropertyAttribute
    {
        public float Min { get; }
        public float Max { get; }
        public float Step { get; }

        public SteppedMinMaxSliderAttribute(float min, float max, float step)
        {
            Min = min;
            Max = max;
            Step = step;
        }
    }
}
