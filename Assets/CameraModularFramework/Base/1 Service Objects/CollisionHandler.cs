using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    [System.Serializable]
    public abstract class CollisionHandler : ScriptableObject
    {
        [HideInInspector] public Vector3[] collisionPoints;
        [HideInInspector] public CameraController cameraController;
        [Header("Basic Settings")]
        [SerializeField, HelpBox("All translations are passed to this module before they are performed in the camera transform.\n" +
        "It checks if it there is a collision along the way of the target object and the future position.\n" +
        "If so, it provides new coordinates avoiding the collision or cliping.", HelpBoxType.None)]
        [Tooltip("If disabled, no collision with be check")]
        public bool enableModule = true;

        public abstract void StartModule();

        public abstract Vector3 CollisionTreatment(Vector3 currentPosition, Vector3 deltaTranslate);
    }
}
