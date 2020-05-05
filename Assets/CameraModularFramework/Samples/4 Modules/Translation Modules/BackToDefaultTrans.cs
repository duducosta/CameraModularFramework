using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{

    public class BackToDefaultTrans : TranslateModule
    {
        private float deltaTime;
        private float timer = 0;
        private float idleTimer = 0;
        private float movementTimer = 0;
        private Vector3 futurePosition;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [SerializeField]
        [Tooltip("...")]
        private bool timedTrigger;
        [SerializeField]
        [Tooltip("...")]
        private float maxTimer;
        [SerializeField]
        [Tooltip("...")]
        private bool idleAndtimerTrigger;
        [SerializeField]
        [Tooltip("...")]
        private float maxIdleTimer;
        [SerializeField]
        [Tooltip("...")]
        private bool movementAndtimerTrigger;
        [SerializeField]
        [Tooltip("...")]
        private float maxMovementTimer;
        [SerializeField]
        [Tooltip("Max distance for each frame for the camera to follow")]
        private float maxDistancePerFrame;
        [SerializeField, Tooltip("Distance is X axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float xOffset;
        [SerializeField, Tooltip("Distance is Y axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float yOffset;
        [SerializeField, Tooltip("Distance is Z axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float zOffset;


        public override void RunModule()
        {
            RunTrigger(timedTrigger, ref timer, maxTimer);
            RunTrigger(idleAndtimerTrigger, ref idleTimer, maxIdleTimer);
            RunTrigger(movementAndtimerTrigger, ref movementTimer, maxMovementTimer);

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

        private void RunTrigger(bool trigger, ref float refTimer, float maxTimer)
        {
            if (trigger)
            {
                refTimer += DeltaTime();
                if (CheckMaxTimer(ref timer, maxTimer))
                {
                    BackToDefault();
                }
            }
        }
        private float DeltaTime()
        {
            if (cameraController.updateMode == UpdateMode.FixedUpdate || cameraController.updateMode == UpdateMode.LateUpdate)
            {
                return Time.fixedDeltaTime;
            }
            else
            {
                return Time.deltaTime;
            }
        }

        private bool CheckMaxTimer(ref float refTimer, float maxTimer)
        {
            if (refTimer >= maxTimer)
            {
                Debug.Log("timer: " + timer);
                refTimer = 0;  //Retirar daqui. Deixar isso no state exit somente, para acinar sempre após ter iniciado
                return true;
            }
            return false;
        }

        private void BackToDefault()
        {
            futurePosition.x = cameraController.mainObject.transform.position.x - xOffset;
            futurePosition.y = cameraController.mainObject.transform.position.y - yOffset;
            futurePosition.z = cameraController.mainObject.transform.position.z - zOffset;
            cameraController.transform.position = Vector3.MoveTowards(cameraController.transform.position, futurePosition, 2);
        }

    }
}
