using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class BackToHeight : TranslateModule
    {
        private Vector3 currentPositionFromMainObj;
        private Vector3 targetPosition;
        private float currentDelta;
        private Vector3 offSet;

        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [SerializeField, Tooltip("Distance is Y axis camera will be positioned relative to the Main Object")]
        private float yOffset;
        [SerializeField, Tooltip("Distance is Z axis camera will be positioned relative to the Main Object")]
        private float zOffset;
        [SerializeField, Tooltip("Speed that the camera will adjust")]
        private float speed;


        public override void RunModule()
        {
            TranslateOutput = Vector3.zero;
            currentPositionFromMainObj = cameraController.transform.position - cameraController.mainObject.transform.position;
            targetPosition = cameraController.mainObject.transform.TransformVector(offSet);
            currentDelta = Vector3.ProjectOnPlane(targetPosition - currentPositionFromMainObj, cameraController.mainObject.transform.up).magnitude;
            if (currentDelta > yOffset * 0.1f)
            {
                Debug.Log("y");
                TranslateOutput = (targetPosition - currentPositionFromMainObj).normalized * speed * cameraController.DeltaTime();
            }
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
            offSet = new Vector3(0, yOffset, -zOffset);

        }
    }
}
