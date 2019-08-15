using UnityEditor;
using NebusokuEngine.FaceEmotion;

namespace NebusokuEngine.Editor.FaceEmotion
{

    public static class IBlinkEntityInspector
    {
        public static float EyeOpenSpeedEditor(this IBlinkEntity entity)
        {
            return GUIEditor(nameof(entity.EyeOpenSpeed), entity.EyeOpenSpeed);
        }
        public static float EyeOpenIntervalEditor(this IBlinkEntity entity)
        {
            return GUIEditor(nameof(entity.EyeOpenInterval), entity.EyeOpenInterval);
        }
        public static float EyeOpenLEditor(this IBlinkEntity entity)
        {
            return GUIEditor(nameof(entity.EyeOpenL), entity.EyeOpenL);
        }
        public static float EyeOpenREditor(this IBlinkEntity entity)
        {
            return GUIEditor(nameof(entity.EyeOpenR), entity.EyeOpenR);
        }
        public static float EyeOpenTimeEditor(this IBlinkEntity entity)
        {
            return GUIEditor(nameof(entity.EyeOpenTime), entity.EyeOpenTime);
        }
        public static float GUIEditor(string name, float value)
        {
            return EditorGUILayout.Slider(name, value, 0, 100);
        }
    }
}
