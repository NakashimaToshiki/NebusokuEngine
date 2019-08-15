using UnityEditor;
using UnityEngine;
using NebusokuEngine.FaceEmotion;
using NebusokuEngine.Editor.FaceEmotion;

namespace NebusokuEngine.Editor
{
    [CustomEditor(typeof(FaceMorph))]
    public class FaceMorphInspector : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            FaceMorph script = target as FaceMorph;

            script.skinnedMesh = EditorGUILayout.ObjectField(nameof(script.skinnedMesh), script.skinnedMesh, typeof(SkinnedMeshRenderer), true) as SkinnedMeshRenderer;

            if (script.enabled)
            {
                if (script.skinnedMesh != null)
                {
                    script.BrowDownLeftEditor();
                    script.BrowDownRightEditor();
                    script.BrowUpLeftEditor();
                    script.BrowUpRightEditor();
                    script.BrowInLeftEditor();
                    script.BrowInRightEditor();

                    script.EyeOpenL = script.EyeOpenLEditor();
                    script.EyeOpenR = script.EyeOpenREditor();
                    script.EyeOpenSpeed = script.EyeOpenSpeedEditor();
                    script.EyeOpenInterval = script.EyeOpenIntervalEditor();
                    script.EyeOpenTime = script.EyeOpenTimeEditor();

                    // いらない
                    /*
                    script.EyeOpenLeftEditor();
                    script.EyeOpenRightEditor();
                    script.EyeHalfOpenLeftEditor();
                    script.EyeHalfOpenRightEditor();
                    */
                    script.SmailLeftEditor();
                    script.SmailRightEditor();
                }
            }
        }
    }
}
