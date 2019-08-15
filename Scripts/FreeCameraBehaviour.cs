using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NebusokuEngine
{
    public abstract class FreeCameraBehaviour : MonoBehaviour
    {
        /// <summary>  </summary>
        protected abstract float XValue { get; }

        /// <summary>  </summary>
        protected abstract float YValue { get; }
    }
}