using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    /// <summary>
    /// Fell free to add the states that you need for your project. Please, be aware that for each state you add here, 
    /// you need to provide adequate transitions and trigger for it in the StateController script. 
    /// </summary>
    public enum CameraStates
    {
        BackToDefault,              //Runs the Back to default Module returning the camera to a desired position
        Disabled,                   //Disable the whole asset, it does not destroy or unload anything.
        RegularGamePlay,            //Idle, walking, jumping, etc (no special camera movement)
    }
}