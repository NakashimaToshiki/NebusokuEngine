using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

namespace NebusokuEngine
{
    public class VRSample : MonoBehaviour
    {

        private void Start()
        {
            XRSettings.LoadDeviceByName("OpenVR");
        }

        [ContextMenu("On")]
        private void On()
        {
            XRSettings.enabled = true;
        }

        [ContextMenu("Off")]
        private void Off()
        {
            XRSettings.enabled = false;
        }
    }
}