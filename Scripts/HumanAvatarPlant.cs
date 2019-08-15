using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NebusokuEngine.CreateHumanAvator;

namespace NebusokuEngine
{
    [DefaultExecutionOrder(1000)]
    /// <summary>  </summary>
    public class HumanAvatarPlant : HumanAvatarPlantBehaviour
    {
        /// <summary>
        /// ここでAvatarのboneNameを記入しないと正しく動かない
        /// 記入していない場合にする分かるようにエラーチェックが必要！
        /// </summary>
        [ContextMenu("初期化します")]
        private void Initialize()
        {

            // ボーンを追加してanimatorが崩れる前に先にHumanoid用のボーンをキャッシュする。
            humanAvatar.GenerateCache(animator);

            HumanAvatarInit();
            // ボーン情報のキャッシュ生成
            //humanActor.GenerateCache(); // いらないと思います。


            // Lerpは変えるので消した
            //humanLerpAvatar.Setup(humanActor.humanSkeletonInfos);
        }

        /*
        protected override void Awake()
        {
            //Initialize();

            base.Awake();
        }*/


        [ContextMenu("スケーリング情報の反映")]
        public void SlideScaleBone()
        {
            _humanSlide.GenerateAvatar(humanSlideModel);
        }

        [ContextMenu("AvatarDataをOsibe式モデル用に調整する")]
        public void HumanAvatarInit()
        {
            HumanBoneData[] datas = humanAvatar.humanBoneDatas;
            HumanBoneData data;
            if ((data = datas.First(item => item.FemaleBodyKey == HumanBodyBones.LeftUpperLeg)) != null)
            {
                data.UseDefaultValues = false;
                data.Max = new Vector3(90, 90, 50);
                data.Min = new Vector3(-60, -60, -120);
            }
            if ((data = datas.First(item => item.FemaleBodyKey == HumanBodyBones.RightUpperLeg)) != null)
            {
                data.UseDefaultValues = false;
                data.Max = new Vector3(90, 90, 50);
                data.Min = new Vector3(-60, -60, -120);
            }
            if ((data = datas.First(item => item.FemaleBodyKey == HumanBodyBones.LeftUpperArm)) != null)
            {
                data.UseDefaultValues = false;
                data.Max = new Vector3(120, 100, -60);
                data.Min = new Vector3(-90, -100, -100);
            }
            if ((data = datas.First(item => item.FemaleBodyKey == HumanBodyBones.RightUpperArm)) != null)
            {
                data.UseDefaultValues = false;
                data.Max = new Vector3(120, 100, -60);
                data.Min = new Vector3(-90, -100, -100);
            }
            if ((data = datas.First(item => item.FemaleBodyKey == HumanBodyBones.Jaw)) != null)
            {
                data.UseDefaultValues = false;
                data.Max = new Vector3(0, 10, 10);
                data.Min = new Vector3(0, -10, 0);
            }

            // LowerLeg
            /*
            humanBoneDatas[3].UseDefaultValues = false;
            humanBoneDatas[3].Max = new Vector3(90, 0, 80);
            humanBoneDatas[3].Min = new Vector3(-130, 0, -80);
            humanBoneDatas[4].UseDefaultValues = false;
            humanBoneDatas[4].Max = new Vector3(90, 0, 80);
            humanBoneDatas[4].Min = new Vector3(-130, 0, -80);*/
        }
    }
}