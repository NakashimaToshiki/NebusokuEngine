using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine.FaceEmotion
{
    public interface IBlinkSetting
    {
        float ThresholdHalf { get; }
        float ThresholdOpen { get; }
    }

    public class BlinkSetting : IBlinkSetting
    {
        public float ThresholdHalf => 50;
        public float ThresholdOpen => 70;
    }
}