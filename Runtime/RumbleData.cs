using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RumbleController
{
    [CreateAssetMenu(fileName = "RumbleData", menuName = "RumbleData", order = 0)]
    public class RumbleData : ScriptableObject
    {
        public AnimationCurve lowFrequencyCurve;
        public AnimationCurve highFrequencyCurve;
        public float duration;
    }
}