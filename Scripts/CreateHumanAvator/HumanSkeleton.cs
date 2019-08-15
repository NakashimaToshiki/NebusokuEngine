using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace NebusokuEngine.CreateHumanAvator
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Utilにしようとしたら、メソッドの引数が多くなったので諦めた</remarks>
    public class HumanSkeleton
    {
        readonly Animator _animator;
        readonly ICollection<SkeletonInfo> _skeletonInfos;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="skeletonInfos">この値と参照値が一致するようにする</param>
        /// <remarks></remarks>
        public HumanSkeleton(Animator animator, ICollection<SkeletonInfo> skeletonInfos)
        {
            this._animator = animator;
            this._skeletonInfos = skeletonInfos;
        }

        /// <summary> 
        /// ボーン情報のキャッシュを生成
        /// </summary>
        public ICollection<SkeletonInfo> GenerateCache()
        {
            // !!!!!!!_cache.GenerateCache();
            // 既にリストの存在しているボーンを探索しながらボーンをリストに追加する。

            var boneList = new Dictionary<Transform, SkeletonInfo>();

            // 一番の親ボーンを追加
            AddSkelton(_animator.transform, boneList);

            // 子から親を検索して次々にボーン情報を追加していく、
            // 既に追加しているボーンがあると検索を停止する。
            Enum.GetValues(typeof(HumanBodyBones)).Cast<HumanBodyBones>().
                Where(_ => _ != HumanBodyBones.LastBone).ToList().    // GetBoneTransformメソッドの引数にLastBoneを入れるとエラーになるため除外
                ForEach(_ => AddParentSkelton(_animator.GetBoneTransform(_), boneList));


            // 検索したボーンを配列化する
            var HumanSkeletonInfos = new SkeletonInfo[boneList.Count];
            int idx = 0;
            foreach (var item in boneList)
            {
                HumanSkeletonInfos[idx++] = item.Value;
            }
            return HumanSkeletonInfos;
        }

        /// <summary> 
        /// 親ボーンを検索 
        /// </summary>
        private void AddParentSkelton(Transform child, Dictionary<Transform, SkeletonInfo> boneList)
        {
            if (child == null) return;

            if (!boneList.ContainsKey(child))
            {
                AddSkelton(child, boneList);
                AddParentSkelton(child.parent, boneList);
            }
        }

        /// <summary> 
        /// ボーン情報の登録 
        /// </summary>
        private void AddSkelton(Transform bone, Dictionary<Transform, SkeletonInfo> boneList)
        {
            boneList.Add(bone, _skeletonInfos.First(item => item.transform == bone));
        }
    }

    public static class HumanAvatarUtil
    {

        /// <summary> 
        /// animatorのAvatarを更新する 
        /// </summary>
        public static void UpdateAnimatorAvatar(Animator animator, HumanDescription humanDescription, ICollection<SkeletonInfo> HumanSkeletonInfos, string avatarName = "customAvater")
        {
            Avatar avater = GenerateAvatar(animator.transform, humanDescription, HumanSkeletonInfos);

            avater.name = avatarName;
            animator.avatar = avater;

            // 一度ポーズを更新しないとおかしなポーズになる。
            // たぶん更新前のavatarにおいての位置・回転情報が残ったままなので変になる？
            var PoseHandler = new HumanPoseHandler(animator.avatar, animator.transform);
            var pose = new HumanPose();
            PoseHandler.GetHumanPose(ref pose);
            PoseHandler.SetHumanPose(ref pose);
        }

        /// <summary> 
        /// Avatarを生成する
        /// </summary>
        public static Avatar GenerateAvatar(Transform root, HumanDescription humanDescription, ICollection<SkeletonInfo> HumanSkeletonInfos)
        {
            List<SkeletonBone> skeletonList = new List<SkeletonBone>();
            HumanSkeletonInfos.ToList().ForEach(_ => skeletonList.Add(_.GetSkeletonBone()));
            var skleton = skeletonList.ToArray();

            humanDescription.skeleton = skleton;

            // AvatarBuilder生成時はanimatorがあるコンポーネントは「parent = null」にする必要がある
            Transform tmp_pare = root.parent; // tmp保存
            root.parent = null; // 親なしにする

            var ret = AvatarBuilder.BuildHumanAvatar(root.gameObject, humanDescription);

            root.parent = tmp_pare; // 親を元に戻す

            // 要らないのでは？
            //humanDescription.skeleton = skleton;
            /*
            // unity2017から仕様が変わったのか、avatar更新後に初期値を設定する必要があるようになった
            // ⇒ 修正。HumanPoseHandlerで設定したほうが直接的に修正できる
            foreach (var info in HumanSkeletonInfos)
            {
                info.transform.localPosition = info.Position;
                info.transform.localRotation = info.Rotation;
            }*/


            return ret;
        }

        /*
        /// <summary> 初期回転値にする </summary>
        public void DefualtPose()
        {
            foreach (var info in HumanSkeletonInfos)
            {
                info.transform.localRotation = info.Rotation;
            }
        }*/
    }
}