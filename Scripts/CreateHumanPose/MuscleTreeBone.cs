namespace NebusokuEngine.CreateHumanPose
{

    /// <summary> 
    /// Humanoidの木構造情報 
    /// </summary>
    public class MuscleTreeBone
    {

        /// <summary> 同じボーンに属するキー </summary>
        public int[] Keys { get; set; }

        /// <summary> 基本と対となるキー </summary>
        public int[] Mirrors { get; set; }

        /// <summary> 体の末端に向かって連結するボーン </summary>
        public MuscleTreeBone[] Trees { get; set; }

        /// <summary> コピー方法 </summary>
        private readonly Type type;

        /// <summary> コピー方法 </summary>
        public enum Type
        {
            /// <summary> 反転コピー </summary>
            Copy,
            /// <summary> 入れ替え </summary>
            Trade,
        }

        public MuscleTreeBone(int[] keys, int[] mirrors, Type type)
        {
            this.Keys = keys;
            this.Mirrors = mirrors;
            this.type = type;
        }

        public MuscleTreeBone(int[] keys, int[] mirrors) : this(keys, mirrors, Type.Copy)
        {
        }

        /// <summary> ミラーコピー </summary>
        public void Mirror(float[] muscles)
        {
            Mirror(muscles, type);
        }

        /// <summary> ミラーコピー </summary>
        private void Mirror(float[] muscles, Type type0)
        {
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Mirrors[i] != -1) /////////
                {
                    switch (type0)
                    {
                        case Type.Copy:
                            muscles[Mirrors[i]] = muscles[Keys[i]];
                            break;
                        case Type.Trade:
                            if (Keys[i] == Mirrors[i])
                            {
                                muscles[Keys[i]] = -muscles[Keys[i]];
                            }
                            else
                            {
                                float tmp = muscles[Mirrors[i]];
                                muscles[Mirrors[i]] = muscles[Keys[i]];
                                muscles[Keys[i]] = tmp;
                            }
                            break;
                    }
                }
            }
            foreach (var tree in Trees)
            {
                tree.Mirror(muscles, type0);
            }
        }


    }
}