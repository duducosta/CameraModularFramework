using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class TranslateWithMainObj : TranslateModule
    {
        private Vector3 lastPosition;
        private Vector3 currentPosition;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [Tooltip("If marked, in case the module is disabled and then enabled at run time, it will reposition the camera close the to camera. Otherwise, it will simply restart the translation effect whereever the camera is")]
        public bool alwaysClamp;
        

        public override void StartModule()
        {
            lastPosition = GetMainObjectPosition();
            currentPosition = GetMainObjectPosition();
        }

        public override void RunModule()
        {
            UpdateObjectPosition();
            TranslateOutput = currentPosition - lastPosition;
        }

        public override void SetEnabled()
        {
            if (!alwaysClamp)
            {
                currentPosition = GetMainObjectPosition();
                UpdateObjectPosition();
            }
            enableModule = true;
        }

        public override void SetDisabled()
        {
            enableModule = false;
        }

        private Vector3 GetMainObjectPosition()
        {
            return cameraController.mainObject.transform.position;
        }

        private void UpdateObjectPosition()
        {
            lastPosition = currentPosition;
            currentPosition = GetMainObjectPosition();
        }
    }
}
