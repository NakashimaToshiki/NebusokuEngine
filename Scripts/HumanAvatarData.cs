using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NebusokuEngine
{
    [Serializable]
    public class HumanBoneData
    {

        public string HumanName;

        public string BoneName;

        public HumanBodyBones FemaleBodyKey;

        public float AxisLength;

        public Vector3 Center;

        public Vector3 Max;

        public Vector3 Min;

        public bool UseDefaultValues;

        public HumanBoneData()
        {
        }

        public HumanBoneData(HumanBodyBones humanBodyKey, HumanBone obj)
        {
            FemaleBodyKey = humanBodyKey;
            BoneName = obj.boneName;
            HumanName = obj.humanName;

            AxisLength = obj.limit.axisLength;
            Center = obj.limit.center;
            Max = obj.limit.max;
            Min = obj.limit.min;
            UseDefaultValues = obj.limit.useDefaultValues;
        }

        public HumanBone Get()
        {
            HumanBone ret = new HumanBone();
            ret.boneName = BoneName;
            ret.humanName = HumanName;
            ret.limit.axisLength = AxisLength;
            ret.limit.center = Center;
            ret.limit.max = Max;
            ret.limit.min = Min;
            ret.limit.useDefaultValues = UseDefaultValues;
            return ret;
        }
    }

    /// <summary> Humanoid Rigの生成 </summary>
    public class HumanAvatarData : MonoBehaviour
    {
        /// <summary> データ </summary>
        public HumanBoneData[] humanBoneDatas;

        public HumanBoneData this[HumanBodyBones key]
        {
            get
            {
                return humanBoneDatas[(int)key];
            }
        }

        [ContextMenu("初期化します")]
        public void Initialize()
        {
            // HumanoidRigボーンの固有名称を全取得
            string[] boneNames = HumanTrait.BoneName;

            // 設定データ。制限値とかを調整する。
            humanBoneDatas = new HumanBoneData[HumanTrait.BoneCount];

            for (int i = 0; i < boneNames.Length; i++)
            {
                humanBoneDatas[i] = new HumanBoneData();
                humanBoneDatas[i].FemaleBodyKey = (HumanBodyBones)i;
                humanBoneDatas[i].UseDefaultValues = true;
                humanBoneDatas[i].HumanName = boneNames[i];
            }
        }


        /// <summary> 引数のAvatarから設定値を変更します。 </summary>
        public void GenerateCache(Animator anim)
        {
            Initialize();

            // HumanBodyBonesにはLastBoneが存在するので
            // foreach (HumanBodyBones key in Enum.GetValues(typeof(HumanBodyBones)))で回すのは不可
            for (int i = 0; i < humanBoneDatas.Length; i++)
            {
                Transform bone = anim.GetBoneTransform((HumanBodyBones)i);
                if (bone != null)
                {
                    humanBoneDatas[i].BoneName = bone.name;
                }
            }

        }

        /// <summary>  </summary>
        public HumanDescription GetHumanDescription()
        {
            HumanDescription hd = new HumanDescription();

            hd.human = new HumanBone[humanBoneDatas.Length];
            for (int i = 0; i < hd.human.Length; i++)
            {
                hd.human[i] = humanBoneDatas[i].Get();
            }
            return hd;
        }
    }

}