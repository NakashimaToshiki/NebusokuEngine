using UnityEditor;
using NebusokuEngine.FaceEmotion;

namespace NebusokuEngine.Editor.FaceEmotion
{

    public static class IBrowMorphViewInspector
    {
        public static void BrowDownLeftEditor(this IBrowMorphView entity)
        {
            entity.BrowDownLeft = GUIEditor(nameof(entity.BrowDownLeft), entity.BrowDownLeft);
        }
        public static void BrowDownRightEditor(this IBrowMorphView entity)
        {
            entity.BrowDownRight = GUIEditor(nameof(entity.BrowDownRight), entity.BrowDownRight);
        }
        public static void BrowUpLeftEditor(this IBrowMorphView entity)
        {
            entity.BrowUpLeft = GUIEditor(nameof(entity.BrowUpLeft), entity.BrowUpLeft);
        }
        public static void BrowUpRightEditor(this IBrowMorphView entity)
        {
            entity.BrowUpRight = GUIEditor(nameof(entity.BrowUpRight), entity.BrowUpRight);
        }
        public static void BrowInLeftEditor(this IBrowMorphView entity)
        {
            entity.BrowInLeft = GUIEditor(nameof(entity.BrowInLeft), entity.BrowInLeft);
        }
        public static void BrowInRightEditor(this IBrowMorphView entity)
        {
            entity.BrowInRight = GUIEditor(nameof(entity.BrowInRight), entity.BrowInRight);
        }
        public static float GUIEditor(string name, float value)
        {
            return EditorGUILayout.Slider(name, value, 0, 100);
        }
    }
}
