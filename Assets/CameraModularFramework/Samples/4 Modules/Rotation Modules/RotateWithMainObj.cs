using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class RotateWithMainObj : RotateModule
    {
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [SerializeField]
        [Tooltip("Max angle for each frame for the camera to follow")]
        private float maxAnglePerFrame;

        public override void StartModule()
        { 
            //Nothing to do here
        }

        public override void RunModule()
        {
            cameraController.transform.rotation = Quaternion.RotateTowards(cameraController.transform.rotation, cameraController.mainObject.transform.rotation, maxAnglePerFrame);
        }

        public override void SetEnabled()
        {
            enableModule = true;
        }

        public override void SetDisabled()
        {
            enableModule = false;
        }
    }

}
