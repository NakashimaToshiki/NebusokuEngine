using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace NebusokuEngine.CreateHumanAvator
{

    public class HumanScale
    {
        public float HipHeight;

        public float FootHight;

        public float LegLowerHight;

        public float LegUpperHight;

        /// <summary> プロパティキー </summary>
        public enum Key
        {
            Height,
            Hips,
            UpperChest,
            Chest,
            Spine,
            Neck,
            Head,
            Shoulder_L,
            Shoulder_R,
            ArmUpper_L,
            ArmUpper_R,
            ArmLower_L,
            ArmLower_R,
            Hand_L,
            Hand_R,
            LegUpper_L,
            LegUpper_R,
            LegLower_L,
            LegLower_R,
            Foot_L,
            Foot_R
        }

        readonly Animator _animator;

        public HumanScale(Animator animator)
        {
            _animator = animator;
        }

        /// <summary> 最大数 </summary>
        static public int KeyMax { get { return Enum.GetNames(typeof(Key)).Length; } }

        /// <summary> プロパティ </summary>
        public ScaleBone this[Key key]
        {
            get { return scaleBones[(int)key]; }
            set { scaleBones[(int)key] = value; }
        }

        /// <summary> プロパティ </summary>
        public ScaleBone[] scaleBones;

        /// <summary> スケーリング情報をデータクラスに反映させる。 </summary>
        public void GenerateAvatar(IReadOnlyCollection<SkeletonInfo> _humanSkeletonInfos)
        {
            // 
            foreach (SkeletonInfo info in _humanSkeletonInfos)
            {
                info.Scale = info.transform.localScale;
            }

            // Hipボーンの高さを調整。足が地面に着く位置に移動。
            _humanSkeletonInfos.First(item => item.Name == this[Key.Hips].scaleBone.name).Position.z =
                this[Key.LegLower_L].Scale.x * LegLowerHight +
                this[Key.LegUpper_L].Scale.x * LegUpperHight +
                this[Key.Foot_L].Scale.x * FootHight -
                this[Key.Hips].Scale.x * HipHeight; // hipのx方向スケールは変なので、基本いらないが一応

            //_humanSkeleton.GenerateAvatar(humanDescription);
        }

        /// <summary> 調整用ボーンを追加し </summary>
        public void GenerateScaleBone()
        {
            scaleBones = new ScaleBone[KeyMax];

            // 身長変更用ボーンを生成する
            this[Key.Height] = new ScaleBone(_animator.transform);
            this[Key.Hips] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.Hips), _animator.GetBoneTransform(HumanBodyBones.Spine));
            this[Key.Spine] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.Spine), _animator.GetBoneTransform(HumanBodyBones.Chest));
            this[Key.Chest] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.Chest), _animator.GetBoneTransform(HumanBodyBones.UpperChest));
            this[Key.UpperChest] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.UpperChest), _animator.GetBoneTransform(HumanBodyBones.Neck));
            this[Key.Neck] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.Neck), _animator.GetBoneTransform(HumanBodyBones.Head));
            this[Key.Head] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.Head));
            this[Key.Shoulder_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftShoulder), _animator.GetBoneTransform(HumanBodyBones.LeftUpperArm));
            this[Key.Shoulder_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightShoulder), _animator.GetBoneTransform(HumanBodyBones.RightUpperArm));
            this[Key.ArmUpper_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftUpperArm), _animator.GetBoneTransform(HumanBodyBones.LeftLowerArm));
            this[Key.ArmUpper_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightUpperArm), _animator.GetBoneTransform(HumanBodyBones.RightLowerArm));
            this[Key.ArmLower_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftLowerArm), _animator.GetBoneTransform(HumanBodyBones.LeftHand));
            this[Key.ArmLower_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightLowerArm), _animator.GetBoneTransform(HumanBodyBones.RightHand));
            this[Key.Hand_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftHand));
            this[Key.Hand_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightHand));
            this[Key.LegUpper_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg), _animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg));
            this[Key.LegUpper_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightUpperLeg), _animator.GetBoneTransform(HumanBodyBones.RightLowerLeg));
            this[Key.LegLower_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg), _animator.GetBoneTransform(HumanBodyBones.LeftFoot));
            this[Key.LegLower_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightLowerLeg), _animator.GetBoneTransform(HumanBodyBones.RightFoot));
            this[Key.Foot_L] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.LeftFoot));
            this[Key.Foot_R] = new ScaleBone(_animator.GetBoneTransform(HumanBodyBones.RightFoot));

            float armature_h = _animator.GetBoneTransform(HumanBodyBones.Hips).parent.position.y;
            FootHight = _animator.GetBoneTransform(HumanBodyBones.LeftFoot).position.y - armature_h;
            LegLowerHight = _animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg).position.y - armature_h - FootHight;
            LegUpperHight = _animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg).position.y - armature_h - FootHight - LegLowerHight;
            HipHeight = _animator.GetBoneTransform(HumanBodyBones.Hips).position.y - armature_h - FootHight - LegLowerHight - LegUpperHight;

            // ボーン情報のキャッシュ再生成
            //_humanSkeleton.GenerateCache(); // ！オーバーライドして使っていたので不具合あるかも

            // Avatar再生成
            //GenerateAvatar(humanDescription);
        }
    }
}