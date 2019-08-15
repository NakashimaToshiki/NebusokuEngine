using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace NebusokuEngine.CreateHumanAvator
{
    /// <summary> 
    /// 変形情報の値を出力するユーティリティ
    /// </summary>
    public static class SkeletonCacheUtil
    {

        /// <summary>
        /// 指定のボーンにある子ボーンすべての変形情報を出力する。
        /// </summary>
        /// <param name="animatorRoot">この</param>
        /// <returns></returns>
        public static ICollection<SkeletonInfo> GenerateFromRootBone(Transform animatorRoot)
        {
            Dictionary<string, SkeletonInfo> boneList = new Dictionary<string, SkeletonInfo>();

            AddChildSkelton(animatorRoot, boneList);

            var ret = new SkeletonInfo[boneList.Count];

            int idx = 0;
            foreach (var item in boneList)
            {
                ret[idx++] = item.Value;
            }

            return ret;
        }

        /// <summary> 
        /// サブ子ボーンを含めて全検索 
        /// </summary>
        private static void AddChildSkelton(Transform pare, Dictionary<string, SkeletonInfo> boneList)
        {
            AddSkelton(pare, boneList);
            foreach (Transform child in pare)
            {
                AddChildSkelton(child, boneList);
            }
        }

        /// <summary> 
        /// スケルトンの追加 
        /// </summary>
        private static void AddSkelton(Transform trans, Dictionary<string, SkeletonInfo> boneList)
        {
            string name = trans.name;
            if (!boneList.ContainsKey(name))
            {
                SkeletonInfo info = new SkeletonInfo(trans);
                boneList.Add(name, info);
            }
            else
            {
                Debug.Log(name + "は既に存在しています。");
            }

        }
    }
}