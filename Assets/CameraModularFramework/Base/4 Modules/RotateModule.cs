using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    [System.Serializable]
    public abstract class RotateModule : ScriptableObject
    {
        [HideInInspector] public CameraController cameraController;

        [Header("Basic settings (base class)")]
        [HelpBox("This module is the basic ROTATE module.\n" +
            "It MUST make the rotation INSIDE the module, and WITHIN the RunModule() method.\n" +
            "If authoritarian methods are used in this module, they will potencially over write all the other modules inputs or \n" +
            "cause some undesired behaviour"
            , HelpBoxType.None)]
        [Tooltip("If disabled, the Camera Controller will ignore the output of this module\n")]
        public bool enableModule = true;

        public bool IsEnabled()
        {
            return enableModule;
        }

        public abstract void StartModule();
        public abstract void RunModule();
        public abstract void SetEnabled();
        public abstract void SetDisabled();
    }

}
