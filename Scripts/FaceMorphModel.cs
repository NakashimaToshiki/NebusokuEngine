using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NebusokuEngine
{
    // ここはまだ実装していない

    [Serializable]
    public class FaceMorphModel
    {

        public float BrowDownL { get; set; }
        
        public float BrowDownR { get; set; }

        public float BrowUpL { get; set; }

        public float BrowUpR { get; set; }

        public float BrowInL { get; set; }

        public float BrowInR { get; set; }

        public float SmailL { get; set; }

        public float SmailR { get; set; }

        public float EyeOpenL { get; set; }

        public float EyeOpenR { get; set; }

        public float EyeOpenTime { get; set; }

        public float EyeOpenInterval { get; set; }

        public float EyeOpenSpeed { get; set; }

        public FaceMorphModel()
        {
        }
    }
}
