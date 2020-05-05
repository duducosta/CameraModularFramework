using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    /// <summary>
    /// Used to define when the FSM machine with run the modules for its current state.
    /// It is not recommended to add more types here. If done, the correct treatment will have to be done 
    /// in the CameraController script.
    /// Each line is related to a MonoBehavior method.
    /// LateUpdate seems to work best for the camera.
    /// </summary>
    public enum UpdateMode
    {
        LateUpdate,
        FixedUpdate,
        Update
    }
}