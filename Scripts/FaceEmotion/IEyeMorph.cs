using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine.FaceEmotion
{
    public interface IEyeBlinkService
    {
        float EyeHalfOpenLeft { get; }
        float EyeOpenLeft { get; }
        float EyeHalfOpenRight { get; }
        float EyeOpenRight { get; }

        void MorphUpdate(float blinkLeft, float blinkRight);
    }

    public class EyeBlinkService : IEyeBlinkService
    {
        private readonly IBlinkSetting _setting;

        private readonly EyeBlinkObject _left;
        private readonly EyeBlinkObject _right;

        public EyeBlinkService(EyeBlinkObject left, EyeBlinkObject right)
        {
            _left = left;
            _right = right;
        }

        public float EyeHalfOpenLeft => _left.EyeHalfOpen;
        public float EyeOpenLeft => _left.EyeOpen;

        public float EyeHalfOpenRight => _right.EyeHalfOpen;
        public float EyeOpenRight => _right.EyeOpen;

        public void MorphUpdate(float blinkLeft, float blinkRight)
        {
            _left.MorphUpdate(blinkLeft);
            _right.MorphUpdate(blinkRight);
        }

    }

    public class EyeBlinkObject
    {
        private readonly IBlinkSetting _setting;

        public EyeBlinkObject(IBlinkSetting setting)
        {
            _setting = setting;
        }

        public float EyeHalfOpen { get; private set; }

        public float EyeOpen { get; private set; }

        public void MorphUpdate(float blink)
        {
            // 左目
            if (blink < _setting.ThresholdHalf)
            {
                EyeHalfOpen = Mathf.Lerp(0, 100, (blink / _setting.ThresholdHalf));
                EyeOpen = 0;
            }
            else if (blink < _setting.ThresholdOpen)
            {
                float t = (blink - _setting.ThresholdHalf) / (_setting.ThresholdOpen - _setting.ThresholdHalf);
                EyeHalfOpen = Mathf.Lerp(100, (100 - blink), t);
                EyeOpen = Mathf.Lerp(0, _setting.ThresholdOpen, t);
            }
            else
            {
                EyeHalfOpen = 100 - blink;
                EyeOpen = blink;
            }
        }
    }
}