using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine.CreateHumanPose
{
    [DefaultExecutionOrder(1001)]
    public class HumanMuscleBase : MonoBehaviour
    {
        public Animator animator;

        protected HumanPose _pose;

        public HumanPose Pose { get { return _pose; } }

        private HumanPoseHandler PoseHandler;

        public float[] Muscles
        {
            get { return _pose.muscles; }
            set { _pose.muscles = value; }

        }

        /// <summary> アニメーションプロパティ </summary>
        public float this[HumanMuscleKey key]
        {
            get { return _pose.muscles[(int)key]; }
            set { _pose.muscles[(int)key] = value; }
        }

        /// <summary> アニメーションプロパティ </summary>
        public float this[int index]
        {
            get { return _pose.muscles[index]; }
            set { _pose.muscles[index] = value; }
        }

        /// <summary> Humanoid rigの位置 </summary>
        public Vector3 Position
        {
            get { return _pose.bodyPosition; }
            set { _pose.bodyPosition = value; }
        }

        /// <summary> Humanoid rigの回転 </summary>
        public Quaternion Rotation
        {
            get { return _pose.bodyRotation; }
            set { _pose.bodyRotation = value; }
        }

        /// <summary> Humanoid rigの回転 </summary>
        public Vector3 Angle
        {
            get { return _pose.bodyRotation.eulerAngles; }
            set { _pose.bodyRotation.eulerAngles = value; }
        }

        void Reset()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void Awake()
        {
            GetHumanPose();
        }

        public void GetHumanPose()
        {
            PoseHandler = new HumanPoseHandler(animator.avatar, animator.transform);

            _pose = new HumanPose();

            PoseHandler.GetHumanPose(ref _pose);

            for (int i = 0; i < _pose.muscles.Length; i++)
            {
                _pose.muscles[i] = 0;
            }
            Position = new Vector3(0, 1, 0);
            Rotation = Quaternion.identity;
        }

        public void Update()
        {
            PoseHandler.SetHumanPose(ref _pose);
        }

        protected virtual void OnDestroy()
        {
            PoseHandler.Dispose();
        }
    }
}