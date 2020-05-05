using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    [System.Serializable]
    public abstract class InitialOffset : ScriptableObject
    {
        [HideInInspector]
        public CameraController cameraController;
        [Header("Basic settings (base class)")]
        [HelpBox("This module is executed ONLY ONCE at the Start of the gameplay", HelpBoxType.None)]
        [Tooltip("If disabled, the Camera Controller will ignore the output of this module\n")]
        public bool enableModule = true;

        public abstract void StartModule();
    }
}