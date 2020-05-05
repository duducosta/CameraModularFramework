using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class InitialOffset_sample : InitialOffset
    {
        private GameObject target;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [Tooltip("The reference for the transform.Translate that will be applied")]
        public Space space;
        [SerializeField, Tooltip("Distance is X axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float xOffset;
        [SerializeField, Tooltip("Distance is Y axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float yOffset;
        [SerializeField, Tooltip("Distance is Z axis camera will be positioned relative to the player. Influenced by the Space type selectec above.")]
        private float zOffset;
        [Space]
        [SerializeField, Tooltip("ANGLE: the camera will rotate this angle towards around X axis.\nTARGET: Camera will look above the player, at some height to be defined in a variable below")]
        [HelpBox("Depending of what kind of focus, the script will use only one of the two variables below.", HelpBoxType.Info)]
        private TypeOfFocus typeOfFocus;
        [SerializeField, Range(0, 90), Tooltip("Angle the camera will rotate downwards around X axis")]
        private float angleDownwards;
        [SerializeField, Tooltip("Distance directly over the target object the camera will look at. Sometimes looking straight to the target object obstructes too much of the camera view")]
        private float heightOverPlayer;

        private enum TypeOfFocus
        {
            Angle,
            Target
        }

        public override void StartModule()
        {
            target = SetTarget();
            cameraController.transform.rotation = target.transform.rotation;
            cameraController.transform.position = target.transform.position;
            cameraController.transform.Translate(xOffset, yOffset, -zOffset, space);
            SetFocus();
        }


        private void SetFocus()
        {
            if (typeOfFocus == TypeOfFocus.Angle)
            {
                cameraController.transform.Rotate(angleDownwards, 0, 0);
            }

            if (typeOfFocus == TypeOfFocus.Target)
            {
                cameraController.transform.LookAt(target.transform.position
                    + new Vector3(0, heightOverPlayer, 0));
            }
        }

        private GameObject SetTarget()
        {
            if (cameraController.SpecificTarget == null)
            {
                return cameraController.mainObject;
            }
            else
            {
                return cameraController.SpecificTarget;
            }
        }
    }
}
