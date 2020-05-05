using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CameraModularFramework
{
    public class LookAtMainObject : RotateModule
    {
        private Vector3 lookDirection;
        private Quaternion relativeQuaternion;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [Tooltip("Maximum amount of degrees that the camera is allowed to rotate per frame.")]
        [Range(0,360)]
        public float maxDegrees;


        public override void RunModule()
        {
            lookDirection = (cameraController.mainObject.transform.position - cameraController.transform.position).normalized;
            relativeQuaternion = Quaternion.LookRotation(lookDirection);
            cameraController.transform.rotation = Quaternion.RotateTowards(cameraController.transform.rotation, relativeQuaternion, maxDegrees);
        }

        public override void SetDisabled()
        {
            throw new System.NotImplementedException();
        }

        public override void SetEnabled()
        {
            throw new System.NotImplementedException();
        }

        public override void StartModule()
        {
            //Nothing to do here
        }
    }
}