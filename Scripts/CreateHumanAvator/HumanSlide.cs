using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace NebusokuEngine.CreateHumanAvator
{
    [Serializable]
    public class HumanSlideModel
    {
        [Range(-1, 1)]
        public float Gender;
        [Range(-1, 1)]
        public float Height;
        [Range(-1, 1)]
        public float Waist;
        [Range(-1, 1)]
        public float Head;
        [Range(-1, 1)]
        public float ArmThick;
        [Range(-1, 1)]
        public float LegThick;

        public HumanSlideModel()
        {
        }

    }

    /// <summary>  </summary>
    public class HumanSlide
    {
        readonly HumanScale _humanScale;

        public HumanSlide(HumanScale humanScale)
        {
            _humanScale = humanScale;
        }

        /// <summary> 設定値 </summary>
        [SerializeField]
        protected Slide[] slides = new Slide[Enum.GetNames(typeof(Key)).Length];

        /// <summary> 設定値プロパティ </summary>
        protected Slide this[Key key]
        {
            get { return slides[(int)key]; }
            set { slides[(int)key] = value; }
        }

        /// <summary> プロパティキー </summary>
        public enum Key
        {
            /// <summary>  </summary>
            Gender,
            Height,
            Waist,
            Head,
            ArmThick,
            LegThick,
        }

        [System.Serializable]
        protected class Slide
        {
            public string Name;

            [Range(-1, 1)]
            public float Muscle;

            public float Min;

            public float Max;

            public Bone[] bones;

            [System.Serializable]
            public class Bone
            {
                public string Name;

                public ScaleBone boneScale;

                public Vector3 Min;

                public Vector3 Max;

                public Bone(ScaleBone boneScale, Vector3 min, Vector3 max)
                {
                    this.boneScale = boneScale;
                    this.Min = min;
                    this.Max = max;
                }

                /// <summary> 元ボーンのサイズに加算 </summary>
                public void DoneAdd(float muscle)
                {
                    boneScale.Scale += Max * muscle;
                }

                /// <summary> 元ボーンのサイズに減算 </summary>
                public void DoneSub(float muscle)
                {
                    boneScale.Scale += Min * muscle;
                }
            }

            public Slide(string name, Bone[] bones)
            {
                this.Name = name;
                this.bones = bones;

                foreach (var bone in bones)
                {
                    Min -= bone.Min.magnitude;
                    Max += bone.Max.magnitude;
                }
            }

            public void Done()
            {
                foreach (Bone bone in bones)
                {
                    if (Muscle > 0)
                    {
                        bone.DoneAdd(Mathf.Lerp(0, Max, Muscle));
                    }
                    else if (Muscle < 0)
                    {
                        bone.DoneSub(Mathf.Lerp(0, -Min, Muscle));
                    }
                }
            }
        }

        /// <summary> スケーリング情報をデータクラスに反映させる。 </summary>
        public void GenerateAvatar(HumanSlideModel model)
        {
            this[Key.Gender].Muscle = model.Gender;
            this[Key.Height].Muscle = model.Height;
            this[Key.Waist].Muscle = model.Waist;
            this[Key.Head].Muscle = model.Head;
            this[Key.ArmThick].Muscle = model.ArmThick;
            this[Key.LegThick].Muscle = model.LegThick;

            foreach (HumanScale.Key key in Enum.GetValues(typeof(HumanScale.Key)))
            {
                _humanScale[key].Scale = new Vector3(1, 1, 1);
            }

            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                this[key].Done();
            }
        }

        public void GenerateCache()
        {
            this[Key.Gender] = new Slide(
                Key.Gender.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.Chest],
                new Vector3(0, 0, 0), new Vector3(0.1f, 0.2f, 0)),
                new Slide.Bone(_humanScale[HumanScale.Key.Spine],
                new Vector3(0, 0, 0), new Vector3(0.1f, 0.2f, 0)),
                new Slide.Bone(_humanScale[HumanScale.Key.Neck],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),

                new Slide.Bone(_humanScale[HumanScale.Key.Shoulder_L],
                new Vector3(0, 0, 0), new Vector3(0.2f, 0, 0)),
                new Slide.Bone(_humanScale[HumanScale.Key.Shoulder_R],
                new Vector3(0, 0, 0), new Vector3(0.2f, 0, 0)),

                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),

                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                }
                );

            /*
            
            this[Key.Gender] = new Slide(
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.Chest],
                new Vector3(0, 0, 0), new Vector3(0.3f, 0.3f, 0.3f)),
                new Slide.Bone(_humanScale[HumanScale.Key.Spine],
                new Vector3(0, 0, 0), new Vector3(0.2f, 0.2f, 0.2f)),
                new Slide.Bone(_humanScale[HumanScale.Key.Neck],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),

                new Slide.Bone(_humanScale[HumanScale.Key.Shoulder_L],
                new Vector3(0, 0, 0), new Vector3(0.2f, 0, 0)),
                new Slide.Bone(_humanScale[HumanScale.Key.Shoulder_R],
                new Vector3(0, 0, 0), new Vector3(0.2f, 0, 0)),
                
                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),

                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.05f, 0.05f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_L],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_R],
                new Vector3(0, 0, 0), new Vector3(0, 0.1f, 0.1f)),
                }
                );
            */

            this[Key.Height] = new Slide(
                Key.Height.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.Height],
                new Vector3(-0.2f, -0.2f, -0.2f), new Vector3(0.2f, 0.2f, 0.2f)),
                }
                );

            this[Key.Waist] = new Slide(
                Key.Waist.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.Spine],
                new Vector3(0, -0.4f, 0), new Vector3(0, 0.4f, 0)),
                }
                );

            this[Key.Head] = new Slide(
                Key.Head.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.Head],
                new Vector3(-0.1f, -0.1f, -0.1f), new Vector3(0.1f, 0.1f, 0.1f)),
                }
                );

            this[Key.ArmThick] = new Slide(
                Key.ArmThick.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_L],
                new Vector3(0, -0.1f, -0.1f), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmLower_R],
                new Vector3(0, -0.1f, -0.1f), new Vector3(0, 0.1f, 0.1f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_L],
                new Vector3(0, -0.2f, -0.2f), new Vector3(0, 0.2f, 0.2f)),
                new Slide.Bone(_humanScale[HumanScale.Key.ArmUpper_R],
                new Vector3(0, -0.2f, -0.2f), new Vector3(0, 0.2f, 0.2f))
                }
                );

            this[Key.LegThick] = new Slide(
                Key.LegThick.ToString(),
                new Slide.Bone[] {
                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_L],
                new Vector3(0, -0.08f, -0.08f), new Vector3(0, 0.08f, 0.08f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegLower_R],
                new Vector3(0, -0.08f, -0.08f), new Vector3(0, 0.08f, 0.08f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_L],
                new Vector3(0, -0.16f, -0.16f), new Vector3(0, 0.16f, 0.16f)),
                new Slide.Bone(_humanScale[HumanScale.Key.LegUpper_R],
                new Vector3(0, -0.16f, -0.16f), new Vector3(0, 0.16f, 0.16f))
                }
                );

            /*
            // 初期値
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                this[key].muscle = data[key];
            }

            Done();*/
        }

        public void SetData(HumanSlideModel model)
        {
            // リセット
            foreach (HumanScale.Key key in Enum.GetValues(typeof(HumanScale.Key)))
            {
                _humanScale[key].Scale = new Vector3(1, 1, 1);
            }

            model.Gender = this[Key.Gender].Muscle;
            model.Height = this[Key.Height].Muscle;
            model.Waist = this[Key.Waist].Muscle;
            model.Head = this[Key.Head].Muscle;
            model.ArmThick = this[Key.ArmThick].Muscle;
            model.LegThick = this[Key.LegThick].Muscle;

            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                this[key].Done();
            }
        }

        /*
        public HumanSlideModel GetData()
        {
            HumanSlideModel model = new HumanSlideModel();

            this[Key.Gender].Muscle = model.Gender;
            this[Key.Height].Muscle = model.Height;
            this[Key.Waist].Muscle = model.Waist;
            this[Key.Head].Muscle = model.Head;
            this[Key.ArmThick].Muscle = model.ArmThick;
            this[Key.LegThick].Muscle = model.LegThick;

            return model;
        }*/
    }
}