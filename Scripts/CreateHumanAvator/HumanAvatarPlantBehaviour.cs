using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NebusokuEngine.CreateHumanAvator;

namespace NebusokuEngine
{
    [DefaultExecutionOrder(1000)]
    /// <summary>  </summary>
    public abstract class HumanAvatarPlantBehaviour : MonoBehaviour
    {

        public Animator animator;

        public HumanAvatarData humanAvatar;

        public HumanSlideModel humanSlideModel;

        //protected SkeletonCache _skeletonCache;
        protected HumanSkeleton _humanSkeleton;
        protected HumanSlide _humanSlide;
        protected HumanScale _humanScale;

        protected SkeletonInfo[] _skeletonInfos;
        protected SkeletonInfo[] _humanSkeletonInfos;


        virtual protected void Awake()
        {

            _humanScale = new HumanScale(animator);
            _humanScale.GenerateScaleBone();

            _humanSlide = new HumanSlide(_humanScale);
            _humanSlide.GenerateCache();

            _humanSlide.GenerateAvatar(humanSlideModel);

            //humanAvatar.GenerateCache(animator);

            _skeletonInfos = SkeletonCacheUtil.GenerateFromRootBone(animator.transform).ToArray();

            _humanSkeleton = new HumanSkeleton(animator, _skeletonInfos);
            _humanSkeletonInfos = _humanSkeleton.GenerateCache().ToArray();


            _humanScale.GenerateAvatar(_humanSkeletonInfos); // これはUpdateAnimatorAvatarをする前に必ず必要？

            // ここがHumanSkeletonのGenerateAvatarに該当
            HumanAvatarUtil.UpdateAnimatorAvatar(animator, humanAvatar.GetHumanDescription(), _humanSkeletonInfos);

            // ここまでがHumanArmature.AwakeのGenerateScaleBoneコール

            // 昔のソースコードだとここに記述してあったが、正しくは上のほうだと思う。
            //_humanSlide.GenerateAvatar(humanSlideModel);

        }

        /*
        [ContextMenu("スケーリング情報の反映")]
        public void SlideScaleBone()
        {
            GenerateAvatar(humanSlideModel, humanAvatar.GetHumanDescription());
        }*/
    }
}