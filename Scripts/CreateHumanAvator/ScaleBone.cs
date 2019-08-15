using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace NebusokuEngine.CreateHumanAvator
{
    /// <summary> 人型のボーンのスケーリング </summary>
    [Serializable]
    public class ScaleBone
    {
        /// <summary> インスペクターで表示 </summary>
        public string Name;

        /// <summary> 対象ボーン </summary>
        public Transform scaleBone;

        /// <summary> 調整ボーン </summary>
        public Transform reScaleBone;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scaleBone">スケール対象のボーン</param>
        /// <param name="arget">スケール対象の次のボーン</param>
        public ScaleBone(Transform scaleBone, Transform target)
        {
            Name = scaleBone.name;

            this.scaleBone = scaleBone;

            // 調整用ボーンを生成
            reScaleBone = new GameObject("Slide_" + scaleBone.name).transform;
            reScaleBone.position = target.position;
            reScaleBone.rotation = scaleBone.rotation;
            reScaleBone.parent = scaleBone;

            // 調整用ボーンをスケール対象ボーンとその子ボーンの間に入れる。
            for (int i = scaleBone.childCount - 1; i >= 0; i--)
            {
                scaleBone.GetChild(i).parent = reScaleBone;
            }

        }

        /// <summary> 調整用のボーンなし。体の末端（頭・手・足）で利用 </summary>
        public ScaleBone(Transform scaleBone)
        {
            Name = scaleBone.name;
            this.scaleBone = scaleBone;
            reScaleBone = null;
        }

        /// <summary> ボーンのリサイズ。子にあたる調整用ボーンが、逆にリサイズする。 </summary>
        public Vector3 Scale
        {
            get
            {
                return scaleBone.localScale;
            }
            set
            {
                scaleBone.localScale = value;
                if (reScaleBone != null)
                {
                    reScaleBone.transform.localScale = new Vector3(1 / value.x, 1 / value.y, 1 / value.z);
                }
            }
        }
    }
}
