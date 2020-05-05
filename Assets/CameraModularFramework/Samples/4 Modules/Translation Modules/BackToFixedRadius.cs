using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{

    public class BackToFixedRadius : TranslateModule
    {
        private Vector3 currentPositionFromMainObj;
        private Vector3 targetPosition;
        private float radiusMagnitude;

        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [SerializeField, Tooltip("If true, the radius considered will be the one set at the initial offset module")]
        private bool useInitialRadius;
        [SerializeField, Tooltip("Distance the camera will be positioned relative to the player.\n" +
            " This will be IGNORED if the above setting is set to TRUE")]
        private float radius;


        public override void RunModule()
        {
            TranslateOutput = Vector3.zero;
            currentPositionFromMainObj = cameraController.transform.position - cameraController.mainObject.transform.position;
            targetPosition = currentPositionFromMainObj.normalized * radiusMagnitude;
            if (currentPositionFromMainObj.magnitude < radiusMagnitude * 0.9f)
            {
                TranslateOutput = currentPositionFromMainObj.normalized;
            }
            if (currentPositionFromMainObj.magnitude > radiusMagnitude * 1.1f)
            {
                TranslateOutput = -currentPositionFromMainObj.normalized;
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
            if (useInitialRadius)
            {
                radiusMagnitude = (cameraController.transform.position - cameraController.mainObject.transform.position).magnitude;
            }
            else
            {
                radiusMagnitude = radius;
            }
        }
    }
}