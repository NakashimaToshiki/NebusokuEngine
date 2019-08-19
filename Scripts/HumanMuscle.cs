using NebusokuEngine.CreateHumanPose;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NebusokuEngine
{

    public class HumanMuscle : HumanMuscleBase
    {
        public HumanMuscleScriptableObject scriptableObject;

        [ContextMenu("保存します")]
        private void Save()
        {
            if (scriptableObject == null) throw new NullReferenceException($"{nameof(scriptableObject)}を設定してください。");
            scriptableObject.postion = this.Position;
            scriptableObject.rotation = this.Rotation;
            scriptableObject.muscles = this.Muscles;
#if UNITY_EDITOR
            EditorUtility.SetDirty(scriptableObject);
            AssetDatabase.SaveAssets();
#endif
        }

        public override void Awake()
        {
            base.Awake();

            if (scriptableObject == null) return;
            this.Position = scriptableObject.postion;
            this.Rotation = scriptableObject.rotation;
            this.Muscles = scriptableObject.muscles;
        }
    }
}