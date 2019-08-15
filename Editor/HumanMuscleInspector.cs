using System;
using UnityEditor;
using UnityEngine;
using NebusokuEngine.CreateHumanPose;

namespace NebusokuEngine.Editor
{
    [CustomEditor(typeof(HumanMuscle))]
    public class HumanMuscleInspector : UnityEditor.Editor
    {
        private IHumanBone boneName;

        private TransformGUI transGUI = new TransformGUI(1.0f);

        private enum MenuName
        {
            All,
            Body,
            Finger,
        }

        private MenuName menuName = MenuName.Body;

        void OnEnable()
        {
            HumanMuscle script = target as HumanMuscle;

            //HumanMuscleTree.Initialize();

            if (EditorApplication.isPlaying && script.enabled)
            {
                transGUI.Reset(script.Position, script.Angle);
            }
        }

        public override void OnInspectorGUI()
        {
            HumanMuscle script = target as HumanMuscle;

            if (!EditorApplication.isPlaying)
            {
                script.animator = (Animator)EditorGUILayout.ObjectField("Animator", script.animator, typeof(Animator), true);
                script.scriptableObject = (HumanMuscleScriptableObject)EditorGUILayout.ObjectField("ScriptableObject", script.scriptableObject, typeof(HumanMuscleScriptableObject), true);
            }
            else
            {
                menuName = (MenuName)EditorGUILayout.EnumPopup("MenuName", menuName);

                switch (menuName)
                {
                    case MenuName.All:
                        ViewAllMenu(script);
                        break;
                    case MenuName.Body:
                        ViewBodyMenu(script);
                        break;
                    case MenuName.Finger:
                        ViewFingerMenu(script);
                        break;
                }

                if (menuName == MenuName.All || boneName == RootBone.GetInstance())
                {
                    // 位置と角度
                    script.Position = transGUI.Position(script.Position);
                    script.Angle = transGUI.Angle();
                }
                else
                {
                    if (boneName != null)
                    {
                        foreach (var key in boneName.Muscles)
                        {
                            var id = (HumanMuscleKey)key.Id;
                            script[id] = EditorGUILayout.Slider((id).ToString(), script[id], -1, 1);
                        }

                        // 反転コピー
                        if (GUILayout.Button("Mirror"))
                        {
                            boneName.Mirror(script.Muscles);
                        }
                    }

                }

            }

            EditorUtility.SetDirty(target);
        }

        private void ViewAllMenu(HumanMuscle script)
        {
            if (GUILayout.Button("GetHumanPose")) script.GetHumanPose();
            for (int i = 0; i < Enum.GetNames(typeof(HumanMuscleKey)).Length; i++)
            {
                script.Pose.muscles[i] = EditorGUILayout.Slider(Enum.GetName(typeof(HumanMuscleKey), i), script.Pose.muscles[i], -1, 1);
            }

        }

        private void ViewBodyMenu(HumanMuscle script)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            BoneNameButton(HeadBone.GetInstance(), 100);
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            BoneNameButton(ShoulderLeftBone.GetInstance());
            BoneNameButton(NeckBone.GetInstance(), 80);
            BoneNameButton(ShoulderRightBone.GetInstance());
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            BoneNameButton(UpperArmLeftBone.GetInstance());
            EditorGUILayout.Space();
            BoneNameButton(UpperChestBone.GetInstance(), 100);
            EditorGUILayout.Space();
            BoneNameButton(UpperArmRightBone.GetInstance());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            BoneNameButton(LowerArmLeftBone.GetInstance());
            EditorGUILayout.Space();
            BoneNameButton(ChestBone.GetInstance(), 100);
            EditorGUILayout.Space();
            BoneNameButton(LowerArmRightBone.GetInstance());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            BoneNameButton(HandLeftBone.GetInstance(), 100);
            EditorGUILayout.Space();
            BoneNameButton(SpineBone.GetInstance(), 100);
            EditorGUILayout.Space();
            BoneNameButton(HandRightBone.GetInstance(), 100);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            BoneNameButton(RootBone.GetInstance(), 100);
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            BoneNameButton(UpperLegLeftBone.GetInstance(), 150);
            BoneNameButton(UpperLegRightBone.GetInstance(), 150);
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            BoneNameButton(LowerLegLeftBone.GetInstance(), 150);
            EditorGUILayout.Space();
            BoneNameButton(LowerLegRightBone.GetInstance(), 150);
            EditorGUILayout.Space();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            BoneNameButton(FootLeftBone.GetInstance(), 150);
            EditorGUILayout.Space();
            BoneNameButton(FootRightBone.GetInstance(), 150);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            BoneNameButton(ToesLeftBone.GetInstance(), 150);
            EditorGUILayout.Space();
            BoneNameButton(ToesRightBone.GetInstance(), 150);
            GUILayout.EndHorizontal();

            /*
            foreach (HumanMuscleTree.BoneKey b in Enum.GetValues(typeof(HumanMuscleTree.BoneKey)))
            {
                if (b == boneName)
                {
                    var bone = HumanMuscleTree.bones[(int)boneName];
                    for (int i = 0; i < bone.keys.Length; i++)
                    {
                        var key = bone.keys[i];
                        script[key] = EditorGUILayout.Slider(key.ToString(), script[key], -1, 1);
                    }

                    if (GUILayout.Button("Mirror"))
                    {
                        HumanMuscleTree.bones[(int)boneName].Mirror(script);
                    }
                }
            }*/

        }

        private void BoneNameButton(IHumanBone name, int width)
        {
            if (GUILayout.Button(((BoneKey)name.Id).ToString(), GUILayout.Width(width), GUILayout.Height(20))) boneName = name;
        }

        private void BoneNameButton(IHumanBone name)
        {
            if (GUILayout.Button(((BoneKey)name.Id).ToString())) boneName = name;
        }

        private void ViewFingerMenu(HumanMuscle script)
        {
            GUILayout.BeginHorizontal();
            BoneNameButton(ThumbLeftBone.GetInstance());
            BoneNameButton(IndexLeftBone.GetInstance());
            BoneNameButton(IndexLeftBone.GetInstance());
            BoneNameButton(RingLeftBone.GetInstance());
            BoneNameButton(LittleLeftBone.GetInstance());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            BoneNameButton(ThumbRightBone.GetInstance());
            BoneNameButton(IndexRightBone.GetInstance());
            BoneNameButton(IndexRightBone.GetInstance());
            BoneNameButton(RingRightBone.GetInstance());
            BoneNameButton(LittleRightBone.GetInstance());
            GUILayout.EndHorizontal();

            /*
            var bone = HumanMuscleTree.bones[(int)boneName];

            foreach (var key in bone.keys)
            {
                script[key] = EditorGUILayout.Slider(key.ToString(), script[key], -1, 1);
            }

            // 反転コピー
            if (GUILayout.Button("Mirror"))
            {
                HumanMuscleTree.bones[(int)boneName].Mirror(script);
            }*/
        }


        public class TransformGUI
        {
            /// <summary> 位置のスライダー初期値 </summary>
            private Vector3 pos_slider;

            /// <summary> 角度のスライダー初期値 </summary>
            private Vector3 angle_slider;

            private readonly float limit;

            public TransformGUI(float limit)
            {
                this.limit = limit;

            }

            public void Reset(Vector3 pos, Vector3 angle)
            {
                // 位置初期値
                pos_slider = pos;

                // 角度初期値
                // 0~360→ -180~180 に変換する
                angle_slider = angle;
                if (angle_slider.x > 180) angle_slider.x -= 360;
                if (angle_slider.y > 180) angle_slider.y -= 360;
                if (angle_slider.z > 180) angle_slider.z -= 360;
            }

            /// <summary> 位置 </summary>
            public Vector3 Position(Vector3 pos)
            {
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Pos");
                if (GUILayout.Button("OK")) pos_slider = pos;
                GUILayout.EndHorizontal();
                pos.x = EditorGUILayout.Slider("X", pos.x, pos_slider.x - limit, pos_slider.x + limit);
                pos.y = EditorGUILayout.Slider("Y", pos.y, pos_slider.y - limit, pos_slider.y + limit);
                pos.z = EditorGUILayout.Slider("Z", pos.z, pos_slider.z - limit, pos_slider.z + limit);
                return pos;

            }

            /// <summary> 角度 </summary>
            public Vector3 Angle()
            {
                EditorGUILayout.LabelField("Angle");
                angle_slider.x = EditorGUILayout.Slider("X", angle_slider.x, -180, 180);
                angle_slider.y = EditorGUILayout.Slider("Y", angle_slider.y, -180, 180);
                angle_slider.z = EditorGUILayout.Slider("Z", angle_slider.z, -180, 180);
                return angle_slider;
            }
        }
    }
}