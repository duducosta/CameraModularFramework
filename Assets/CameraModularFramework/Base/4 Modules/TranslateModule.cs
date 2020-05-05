using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    [System.Serializable]
    public abstract class TranslateModule : ScriptableObject
    {
        [HideInInspector] public CameraController cameraController;
        [HideInInspector] public Vector3 TranslateOutput;

        [Header("Basic settings (base class)")]
        [HelpBox("This module is the basic TRANSLATE module.\n" +
            "It MUST provide a Vector3 with the amount of movement for ONE update ONLY.\n" +
            "If authoritarian methods are used in this module, they will potencially over write all the other modules inputs or \n" +
            "cause some undesired behaviour"
            ,HelpBoxType.None)]
        [Tooltip("If disabled, the Camera Controller will ignore the output of this module\n")]
        public bool enableModule = true;
        [Space]
        [Tooltip("Determines if the translation will be applied for the X axis")]
        public bool applyToX = true;
        [Tooltip("Determines if the translation will be applied for the Y axis")]
        public bool applyToY = true;
        [Tooltip("Determines if the translation will be applied for the Z axis")]
        public bool applyToZ = true;



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
