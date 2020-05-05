using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{

    public class OrbitWithMainObj : TranslateModule
    {
        private Vector3 baseVector;             //vector starting in the mainObject and ending in the camera at the start of the update
        private Vector3 auxVector;              //vector starting in the mainObject and ending in the camera at the end of the update
        private Vector3 lastZ;                  //Last Main Object forward axis
        private float deltaZ;                   //The angle between lastZ and current main Object forward

        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [HelpBox("The ratio of the sphere is determined base on the initial position of the camera relative to the target object\n" +
            "This does not adjusts the rotation of the camera", HelpBoxType.Info)]
        [Tooltip("The speed which the camera will rotate around the target object, acording to the main object rotation")]
        [SerializeField] private float speed;



        public override void StartModule()
        {
            //Nothing to do here
        }

        public override void RunModule()
        {
            UpdateVectors();
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
            deltaZ = Vector3.SignedAngle(lastZ, cameraController.mainObject.transform.forward, Vector3.up);
            lastZ = cameraController.mainObject.transform.forward;
            baseVector = cameraController.transform.position - cameraController.mainObject.transform.position;
            auxVector = cameraController.transform.position - cameraController.mainObject.transform.position;
        }

        public void CalculateDeltaTranslate()
        {
            auxVector = Quaternion.AngleAxis(deltaZ, Vector3.up) * auxVector;
            TranslateOutput = auxVector - baseVector;
        }

        private void RotationToCameraPosition()
        {

        }

    }
}
