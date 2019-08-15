using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine.FaceEmotion
{
    public enum BlinkState
    {
        Closing,
        Close,
        Opening,
        Open,
    }

    public interface IEyeMorphService
    {
        float BlinkLeft { get; }
        float BlinkRight { get; }

        void BlinkUpdate();
    }

    public interface IBlinkEntity
    {
        float EyeOpenSpeed { get; }
        float EyeOpenInterval { get; }
        float EyeOpenTime { get; }
        float EyeOpenL { get; }
        float EyeOpenR { get; }
    }

    public class EyeMorphService : IEyeMorphService
    {

        /// <summary>
        /// 現在の瞬き値
        /// </summary>
        private float blink = 100;

        /// <summary>
        /// 開始時間
        /// </summary>
        private float startTime;

        /// <summary>
        /// 状態遷移情報
        /// </summary>
        private BlinkState state = BlinkState.Close;

        private readonly IBlinkSetting _setting;
        private readonly IBlinkEntity _entity;

        public EyeMorphService(IBlinkSetting setting, IBlinkEntity entity)
        {
            _setting = setting;
            _entity = entity;
            startTime = Time.time;
        }

        public float BlinkLeft { get; private set; }
        public float BlinkRight { get; private set; }

        public void BlinkUpdate()
        {
            float speed = _entity.EyeOpenSpeed / 100 * 2;
            float interval = _entity.EyeOpenInterval / 100 * 10;
            float openTime = (_entity.EyeOpenTime / 100) * interval;
            float add = (Time.time - startTime) * speed;

            switch (state)
            {
                case BlinkState.Closing:
                    blink += add;
                    if (blink > 1)
                    {
                        blink = 1;
                        state = BlinkState.Close;
                        startTime = Time.time;
                    }
                    break;
                case BlinkState.Close:
                    if (startTime + (interval - openTime) < Time.time)
                    {
                        state = BlinkState.Opening;
                    }
                    break;
                case BlinkState.Opening:
                    blink -= add;
                    if (blink < 0)
                    {
                        blink = 0;
                        state = BlinkState.Open;
                        startTime = Time.time;
                    }
                    break;
                case BlinkState.Open:

                    if (startTime + openTime < Time.time)
                    {
                        state = BlinkState.Closing;
                    }
                    break;
            }

            BlinkLeft = Mathf.Lerp(_entity.EyeOpenL, 0, blink);
            BlinkRight = Mathf.Lerp(_entity.EyeOpenR, 0, blink);
        }
    }
}