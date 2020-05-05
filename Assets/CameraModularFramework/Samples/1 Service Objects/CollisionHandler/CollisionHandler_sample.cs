using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class CollisionHandler_sample : CollisionHandler
    {
        private RaycastHit hit = new RaycastHit();
        private Vector3 testPosition;
        private Vector3 outputPosition;
        [Header("Specific Settings")]
        [SerializeField, TextArea]
        private string SpecificModuleDescription;
        [SerializeField]
        [Tooltip("Parameter for the Lerp performed between the collision point and the camera position. 0.5 means the middle between the two positions")]
        [Range(0, 1)]
        private float smoothLevel;
        [SerializeField]
        [Tooltip("The distance between the collision points and the camera in the middle. 0.5 should be good for all purposes")]
        private float radius;
        [Header("Debug option")]
        [SerializeField]
        [Tooltip("If set to true, the collision information will be printed at the console")]
        public bool showCollisionInfo;


        public override void StartModule()
        {
            collisionPoints = new Vector3[]
            {
                new Vector3(0, 0, 0),           //central point
                new Vector3(-radius, 0, 0),     //right in x axis
                new Vector3(radius, 0, 0),      //left in x axis
                new Vector3(0, radius, 0),      //above in y axis
                new Vector3(0, 0, -radius),     //behind in z axis
                new Vector3(0, -radius, 0),     //below in y axis
                new Vector3(0, 0, radius)       //front in z axis
            };
        }

        public override Vector3 CollisionTreatment(Vector3 currentPosition, Vector3 deltaTranslate)
        {
            testPosition.x = currentPosition.x + deltaTranslate.x;
            testPosition.y = currentPosition.y + deltaTranslate.y;
            testPosition.z = currentPosition.z + deltaTranslate.z;

            if (enableModule)
            {
                for (int i = 0; i < collisionPoints.Length; i++)
                {
                    if (Physics.Linecast(cameraController.mainObject.transform.position, testPosition + collisionPoints[i], out hit))
                    {
                        outputPosition.x = hit.point.x + hit.normal.x * 0.5f;
                        outputPosition.y = hit.point.y + hit.normal.y * 0.5f;
                        outputPosition.z = hit.point.z + hit.normal.z * 0.5f;
                        outputPosition = Vector3.Lerp(currentPosition, outputPosition, smoothLevel);

                        if (showCollisionInfo)
                        {
                            Debug.Log(this.name + " Collided with: " + hit.collider.name);
                            Debug.DrawLine(cameraController.lastPosition, hit.point);
                        }

                        return outputPosition;
                    }
                }
                outputPosition = testPosition;
                return outputPosition;
            }
            else
            {
                outputPosition = testPosition;
                return outputPosition;
            }

        }
    }
}
