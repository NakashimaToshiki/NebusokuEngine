using UnityEditor;
using NebusokuEngine.FaceEmotion;

namespace NebusokuEngine.Editor.FaceEmotion
{

    public static class IEyeMorphViewInspector
    {
        public static void EyeHalfOpenLeftEditor(this IEyeMorphView entity)
        {
            entity.EyeHalfOpenLeft = GUIEditor(nameof(entity.EyeHalfOpenLeft), entity.EyeHalfOpenLeft);
        }
        public static void EyeOpenLeftEditor(this IEyeMorphView entity)
        {
            entity.EyeOpenLeft = GUIEditor(nameof(entity.EyeOpenLeft), entity.EyeOpenLeft);
        }
        public static void SmailLeftEditor(this IEyeMorphView entity)
        {
            entity.SmailLeft = GUIEditor(nameof(entity.SmailLeft), entity.SmailLeft);
        }

        public static void EyeHalfOpenRightEditor(this IEyeMorphView entity)
        {
            entity.EyeHalfOpenRight = GUIEditor(nameof(entity.EyeHalfOpenRight), entity.EyeHalfOpenRight);
        }
        public static void EyeOpenRightEditor(this IEyeMorphView entity)
        {
            entity.EyeOpenRight = GUIEditor(nameof(entity.EyeOpenRight), entity.EyeOpenRight);
        }
        public static void SmailRightEditor(this IEyeMorphView entity)
        {
            entity.SmailRight = GUIEditor(nameof(entity.SmailRight), entity.SmailRight);
        }

        public static float GUIEditor(string name, float value)
        {
            return EditorGUILayout.Slider(name, value, 0, 100);
        }
    }
}
