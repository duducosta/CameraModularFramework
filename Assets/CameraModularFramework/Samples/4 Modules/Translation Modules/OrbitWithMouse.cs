using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class OrbitWithMouse : TranslateModule
    {
        private Vector3 baseVector;             //vector starting in the mainObject and ending in the camera at the start of the update
        private Vector3 auxVector;              //vector starting in the mainObject and ending in the camera at the end of the update
        private float hor;                      //variable to store the mouse movement
        private float ver;                      //variable to store the mouse movement

        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [HelpBox("The ratio of the sphere is determined base on the initial position of the camera relative to the target object\n" +
            "This does not adjusts the rotation of the camera", HelpBoxType.Info)]
        [Tooltip("The speed which the mouse will rotate around the target object")]
        [SerializeField] private float speed;



        public override void StartModule()
        {
            //Nothing to do here
        }

        public override void RunModule()
        {
            UpdateVectors();
            CaptureMouseMovement();
            CalculateDeltaTranslate();
        }

        public override void SetDisabled()
        {
            throw new System.NotImplementedException();
        }

        public override void SetEnabled()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateVectors()
        {
            baseVector = cameraController.transform.position - cameraController.mainObject.transform.position;
            auxVector = cameraController.transform.position - cameraController.mainObject.transform.position;
        }

        public void CaptureMouseMovement()
        {
            hor = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
            ver = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        }

        public void CalculateDeltaTranslate()
        {
            auxVector = Quaternion.AngleAxis(hor, Vector3.right) * auxVector;
            auxVector = Quaternion.AngleAxis(ver, Vector3.up) * auxVector;
            TranslateOutput = auxVector - baseVector;
        }

    }
}