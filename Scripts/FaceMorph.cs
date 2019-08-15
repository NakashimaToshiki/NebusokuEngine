using UnityEngine;
using NebusokuEngine.FaceEmotion;

namespace NebusokuEngine
{

    public enum MorphKey
    {
        EyeOpenLeft,
        EyeHalfOpenLeft,
        SmailLeft,
        BrowDownLeft,
        BrowUpLeft,
        BrowInLeft,
        EyeOpenRight,
        EyeHalfOpenRight,
        SmailRight,
        BrowDownRight,
        BrowUpRight,
        BrowInRight,
    };

    // IEyeMorphView, IBrowMorphView, IBlinkEntity
    // ここはstructでもいいかもしれない・・
    // いや、Editorの拡張メソッドが使えなくなるかも

    /// <summary>
    /// 表情のコンポーネント
    /// シェイプキーは<see cref="MorphKey"/>列挙型に依存しています。
    /// </summary>
    public class FaceMorph : MonoBehaviour, IEyeMorphView, IBrowMorphView, IBlinkEntity
    {
        public SkinnedMeshRenderer skinnedMesh;

        private EyeBlinkService _eyeBlinkService;
        private EyeMorphService _eyeMorphService;
        private EyeMorphController _eyeMorphController;

        // [分離案]シェイプキーが列挙型に依存している。

        public float this[MorphKey key]
        {
            get
            {
                return skinnedMesh.GetBlendShapeWeight((int)key);
            }
            set
            {
                skinnedMesh.SetBlendShapeWeight((int)key, value);
            }
        }

        // ---------- //

        public float EyeOpenLeft
        {
            get { return this[MorphKey.EyeOpenLeft]; }
            set { this[MorphKey.EyeOpenLeft] = value; }
        } 

        public float EyeOpenRight
        {
            get { return this[MorphKey.EyeOpenRight]; }
            set { this[MorphKey.EyeOpenRight] = value; }
        }

        public float EyeHalfOpenLeft
        {
            get { return this[MorphKey.EyeHalfOpenLeft]; }
            set { this[MorphKey.EyeHalfOpenLeft] = value; }
        }

        public float EyeHalfOpenRight
        {
            get { return this[MorphKey.EyeHalfOpenRight]; }
            set { this[MorphKey.EyeHalfOpenRight] = value; }
        }

        public float SmailLeft
        {
            get { return this[MorphKey.SmailLeft]; }
            set { this[MorphKey.SmailLeft] = value; }
        }

        public float SmailRight
        {
            get { return this[MorphKey.SmailRight]; }
            set { this[MorphKey.SmailRight] = value; }
        }


        public float BrowDownLeft
        {
            get { return this[MorphKey.BrowDownLeft]; }
            set { this[MorphKey.BrowDownLeft] = value; }
        }

        public float BrowDownRight
        {
            get { return this[MorphKey.BrowDownRight]; }
            set { this[MorphKey.BrowDownRight] = value; }
        }

        public float BrowUpLeft
        {
            get { return this[MorphKey.BrowUpLeft]; }
            set { this[MorphKey.BrowUpLeft] = value; }
        }

        public float BrowUpRight
        {
            get { return this[MorphKey.BrowUpRight]; }
            set { this[MorphKey.BrowUpRight] = value; }
        }

        public float BrowInLeft
        {
            get { return this[MorphKey.BrowInLeft]; }
            set { this[MorphKey.BrowInLeft] = value; }
        }

        public float BrowInRight
        {
            get { return this[MorphKey.BrowInRight]; }
            set { this[MorphKey.BrowInRight] = value; }
        }

        public float EyeOpenSpeed { get => _eyeOpenSpeed; set => _eyeOpenSpeed = value; }
        public float _eyeOpenSpeed;

        public float EyeOpenInterval { get => _eyeOpenInterval; set => _eyeOpenInterval = value; }
        public float _eyeOpenInterval;

        public float EyeOpenTime { get => _eyeOpenTime; set => _eyeOpenTime = value; }
        public float _eyeOpenTime;

        public float EyeOpenL { get => _eyeOpenL; set => _eyeOpenL = value; }
        public float _eyeOpenL;

        public float EyeOpenR { get => _eyeOpenR; set => _eyeOpenR = value; }
        public float _eyeOpenR;

        private void Reset()
        {
            skinnedMesh = GetComponent<SkinnedMeshRenderer>() ?? throw new System.NullReferenceException(nameof(skinnedMesh));

            EyeOpenL = 100;
            EyeOpenR = 100;
            EyeOpenSpeed = 30;
            EyeOpenInterval = 30;
            EyeOpenTime = 95;
        }

        // Use this for initialization
        private void Awake()
        {
            var blinkSetting = new BlinkSetting();
            _eyeBlinkService = new EyeBlinkService(new EyeBlinkObject(blinkSetting), new EyeBlinkObject(blinkSetting));
            _eyeMorphService = new EyeMorphService(blinkSetting, this);
            _eyeMorphController = new EyeMorphController(_eyeMorphService, _eyeBlinkService, this);
        }

        private void Update()
        {
            _eyeMorphController.Update();
        }
    }
}
