using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class EventHandler_sample : EventHandler
    {
        private Vector3 lastPosition;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        public override void SetCameraState()
        {
            if (HasInputs() || IsMoving())
            {
                //cameraController.statesArray[cameraController.currentStateIndex].StateTransition(cameraController.statesArray[cameraController.GetStateNumber(CameraStates.RegularGamePlay)]);
                //Fazer um método que chama o State change SOMENTE se for outro estado. Talvez deixar esse método implementado na classe base
            }
            else
            {
                //cameraController.statesArray[cameraController.currentStateIndex].StateTransition(cameraController.statesArray[cameraController.GetStateNumber(CameraStates.BackToDefault)]);
            }
            UpdateData();
        }

        public override void UpdateData()
        {
            lastPosition = cameraController.mainObject.transform.position;
        }

        public bool IsMoving()
        {
            if (cameraController.mainObject.transform.position != lastPosition) { return true; }
            return false;
        }

        private bool HasInputs()
        {
            if (Input.anyKeyDown ||
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void StartModule()
        {
            base.StartModule();
        }
    }
}
