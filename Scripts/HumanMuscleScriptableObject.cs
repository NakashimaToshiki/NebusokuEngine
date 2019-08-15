using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NebusokuEngine.CreateHumanPose;

namespace NebusokuEngine
{
    [CreateAssetMenu(
     fileName = "HumanMuscleData",
     menuName = "ScriptableObject/HumanMuscleScriptableObject",
     order = 0)
   ]
    public class HumanMuscleScriptableObject : ScriptableObject
    {
        public Vector3 postion;
        public Quaternion rotation;
        public float[] muscles;

        public HumanMuscleScriptableObject()
        {
            postion = new Vector3(0, 1, 0);
            rotation = Quaternion.identity;
            muscles = new float[System.Enum.GetValues(typeof(HumanMuscleKey)).Length];
        }
    }
}