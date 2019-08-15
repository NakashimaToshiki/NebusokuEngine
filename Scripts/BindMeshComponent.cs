using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NebusokuEngine
{
    #region Model

    /// <summary>
    /// 変形値の値をキャッシュしておいて、元に戻す値オブジェクトを提供します。
    /// </summary>
    /// <remarks>似たようなものに<see cref="SkeletonBone"/>があるが、struct型で参照できないのでclass型で作った</remarks>
    [Serializable]
    public class SkeletonInfo
    {
        public string Name;

        public Transform transform;

        public Vector3 Position;

        public Quaternion Rotation;

        public Vector3 Scale;

        override public string ToString()
        {
            if (transform == null) return "null";
            else return transform.name;
        }

        public SkeletonInfo(Transform transform)
        {
            this.transform = transform;
            Name = transform.name;

            Initaialize();
        }

        public void Initaialize()
        {
            Position = transform.localPosition;
            Rotation = transform.localRotation;
            Scale = transform.localScale;
        }

        public void Reset()
        {
            transform.localPosition = Position;
            transform.localRotation = Rotation;
            transform.localScale = Scale;
        }

        public SkeletonBone GetSkeletonBone()
        {
            SkeletonBone ret;

            ret.name = Name;

            ret.position = Position;
            ret.rotation = Rotation;
            ret.scale = Scale;

            return ret;
        }

        public override int GetHashCode()
        {
            return this.transform.GetHashCode();
        }

    }

    #endregion

    #region Controller

    public interface IBindWeightMesh
    {
        void BindWeight(SkinnedMeshRenderer meshRenderer);
    }

    public class BindWeightMesh : IBindWeightMesh
    {
        private readonly Transform _rootBone;  //  animator.GetBoneTransform(HumanBodyBones.Hips)

        /// <summary>
        /// ボーン情報
        /// </summary>
        public SkeletonInfo[] skeletonInfos;

        public BindWeightMesh(Animator animator)
        {
            _rootBone = animator.GetBoneTransform(HumanBodyBones.Hips);

            Dictionary<string, SkeletonInfo> boneList = new Dictionary<string, SkeletonInfo>();

            AddChildSkelton(animator.transform, boneList);

            skeletonInfos = new SkeletonInfo[boneList.Count];
            int idx = 0;
            foreach (var item in boneList)
            {
                skeletonInfos[idx++] = item.Value;
            }
        }

        /// <summary> サブ子ボーンを含めて全検索 </summary>
        private void AddChildSkelton(Transform pare, Dictionary<string, SkeletonInfo> boneList)
        {
            AddSkelton(pare, boneList);
            foreach (Transform child in pare)
            {
                AddChildSkelton(child, boneList);
            }
        }

        /// <summary> スケルトンの追加 </summary>
        private void AddSkelton(Transform trans, Dictionary<string, SkeletonInfo> boneList)
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


        /// <summary>
        /// スケルトンとメッシュを接続する
        /// </summary>
        /// <param name="meshRenderer">服などのメッシュ</param>
        public void BindWeight(SkinnedMeshRenderer meshRenderer)
        {
            //
            // 本当はweightIndexも更新すべきだが、面倒なのでパス
            // 3Dソフトのリンク機能を使って、スケルトンを共有したメッシュを使ってね

            Transform[] bones = meshRenderer.bones;

            meshRenderer.rootBone = _rootBone;
            for (int i = 0; i < bones.Length; i++)
            {
                try
                {
                    bones[i] = skeletonInfos.First(item => item.Name == bones[i].name).transform;
                }
                catch (Exception)
                {
                    Debug.Log(meshRenderer.gameObject.name + "の" + bones[i].name + "がない");
                }
            }

            // 代入しないと更新されない
            meshRenderer.bones = bones;
        }
    }

    #endregion

    #region View

    public class BindMeshComponent : MonoBehaviour
    {
        public Animator animator;

        private BindWeightMesh _bindWeightMesh;

        public GameObject clothRoot;

        public SkinnedMeshRenderer[] _meshRenderers;

        private void Awake()
        {
            _bindWeightMesh = new BindWeightMesh(animator);

            foreach (var mesh in _meshRenderers)
            {
                _bindWeightMesh.BindWeight(mesh);
            }
        }

        [ContextMenu("CollectSkinnedMeshs")]
        public void CollectSkinnedMeshs()
        {
            _meshRenderers = clothRoot.GetComponentsInChildren<SkinnedMeshRenderer>();
        }
    }

    #endregion
}
