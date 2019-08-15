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
    public class EnemyStatus : ScriptableObject
    {

        //設定したいデータの変数
        public int HP = 100, SP = 50, Atk = 5, Def = 15, Spd = 99, Exp = 58;
        public string Name = "なまえ";
        public bool IsBoss = false;

        /*簡略化のために全てpublicにしてますが、Scriptableobjectは基本的に変更しないデータを扱うので、
        以下のようにprivateな変数にSerializeFieldを付けて、getterとsetterを別途用意する方が安全です。
        setterは後述する「プログラムから作成」の時に使います。

        [SerializeField]
        private bool _isBoss = false;
        public  bool  IsBoss {
          get { return _isBoss; }
#if UNITY_EDITOR
          set { _isBoss = value; }
#endif
        }

        */

    }

    /*
    [Serializable]
    public class HumanMuscleModel
    {

        [XmlIgnore] public Vector3 postion;
        [XmlIgnore] public Quaternion rotation;
        [XmlIgnore] public Vector3 angle;
        [XmlIgnore] [Range(-1, 1)] public float[] muscles = new float[Enum.GetNames(typeof(HumanMuscleKey)).Length];

        public Vector3 Position
        {
            get { return postion; }
            set { postion = value; }
        }

        public Quaternion Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Vector3 Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public float[] Muscles
        {
            get { return muscles; }
            set { muscles = value; }
        }

        public HumanMuscleModel()
        {
        }
    }*/

    public class HumanMuscle : HumanMuscleBase
    {
        //public HumanMuscleModel Model = new HumanMuscleModel();

        public HumanMuscleScriptableObject scriptableObject;

        [ContextMenu("保存します。")]
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