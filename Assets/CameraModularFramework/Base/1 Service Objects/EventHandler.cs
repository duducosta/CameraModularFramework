using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public abstract class EventHandler : ScriptableObject
    {
        [HideInInspector] public CameraController cameraController;
        [Header("Basis Settings")]
        [Tooltip("Use this to select the first camera state.")]
        public CameraStates initialCameraState;
        public virtual void StartModule()         //This method will be called at the start() method of the cameraController 
        {
            cameraController.cameraState = initialCameraState;
        }

        public abstract void SetCameraState();      //This method will be called everytime the cameraController calls for an update
        public abstract void UpdateData();          //Use this method if your event handler needs to update and store some data everytime the cameraController calls for an update



    }
}
