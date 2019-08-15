using UnityEngine;

namespace NebusokuEngine
{
    public interface ITransformTrack
    {

        void Update(Transform trackBone);
    }

    public class TransformTrack : ITransformTrack
    {

        private readonly Transform _trackedBone;

        public TransformTrack(Transform trackedBone)
        {
            _trackedBone = trackedBone;
        }

        public void Update(Transform trackBone)
        {
            trackBone.position = _trackedBone.position;
            trackBone.rotation = _trackedBone.rotation;
            trackBone.localScale = _trackedBone.localScale;
        }
    }

    public class HairSystem : MonoBehaviour
    {
        private TransformTrack _track;

        public Transform trackedBone;

        public Transform trackBone;

        private void Reset()
        {
            trackedBone = GetComponent<Animator>()?.GetBoneTransform(HumanBodyBones.Head)?.Find("Hair");
        }

        private void Awake()
        {
            _track = new TransformTrack(trackedBone);
        }

        private void Update()
        {
            _track.Update(trackBone);
        }
    }
}