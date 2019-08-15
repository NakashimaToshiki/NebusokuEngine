using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NebusokuEngine.FaceEmotion
{

    public interface IBrowMorphView
    {
        float BrowDownLeft { get; set; }

        float BrowDownRight { get; set; }

        float BrowUpLeft { get; set; }

        float BrowUpRight { get; set; }

        float BrowInLeft { get; set; }

        float BrowInRight { get; set; }

    }

}