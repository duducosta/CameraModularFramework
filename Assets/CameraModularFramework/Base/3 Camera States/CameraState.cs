using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    [System.Serializable]
    public abstract class CameraState : ScriptableObject
    {
        [HideInInspector] public CameraController cameraController;
        [SerializeField] public CameraStates stateName;
        [SerializeField] public TranslateModule[] translateModules;
        [SerializeField] public RotateModule[] rotateModules;

        public void StateTransition(CameraState nextState)
        {
            if (cameraController.cameraState != nextState.stateName) 
            {
                cameraController.statesArray[cameraController.currentStateIndex].OnStateExit();
                cameraController.cameraState = nextState.stateName;
                cameraController.statesArray[cameraController.currentStateIndex].OnStateEntry();
                cameraController.GetStateNumber();
            }
        }

        public abstract void OnStateEntry();
        public abstract void OnStateExit();

    }
}