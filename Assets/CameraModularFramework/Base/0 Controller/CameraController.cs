using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModularFramework
{
    public class CameraController : MonoBehaviour
    {
        private Transform mainObjectTransform;                      //This will be passed to all modules at the StarModules() method
        [HideInInspector] public Vector3 lastPosition;              //only for debug tracing
        private Vector3 deltaTranslateVector;                       //result of all the translations of the current update. Uses the cummulative property of Vector3
        private int statesIndex;                                    //variable used inside for loops. It was pooled just to save the "new" word and reduces garbage collector usage
        private int modulesIndex;                                   //variable used inside for loops. It was pooled just to save the "new" word and reduces garbage collector usage
        [HideInInspector] public CameraStates cameraState;          //stores the current state the camera is on
        [HideInInspector] public int currentStateIndex;             //Stores the position of the state array regarding the current state. Will be read by other scripts.


        [Header("Basic settings")]
        [SerializeField] public UpdateMode updateMode;

        [Header("Setup objects")]
        public GameObject mainObject;
        [Header("Setup objects - Initial Offset")]
        public InitialOffset initialOffset;
        public GameObject SpecificTarget;

        [Header("Service objects")]
        public EventHandler eventHandler;
        public CollisionHandler collisionHandler;

        [Header("Debug Settings")]
        [SerializeField] private bool Trail;
        [SerializeField] private bool ViewDirection;
        [SerializeField] private bool CollisionPoints;
        [SerializeField] private bool PrintState;
        
        [Header("Camera States")]
        [SerializeField] public CameraState[] statesArray;

        private void Awake()
        {
            CheckModulesFound();
        }

        void Start()
        {
            StartModules();
        }

        private void Update()
        {
            if (updateMode == UpdateMode.Update) { ExecuteCameraUpdate(); }
        }

        void FixedUpdate()
        {
            if (updateMode == UpdateMode.FixedUpdate) { ExecuteCameraUpdate(); }
        }

        private void LateUpdate()
        {
            if (updateMode == UpdateMode.LateUpdate) { ExecuteCameraUpdate(); }
        }


        /// <summary>
        /// Check if the objects are not null.
        /// This does not return an custom exception, only warnings.
        /// </summary>
        private void CheckModulesFound()
        {
            //Check service modules
            if (eventHandler == null) { Debug.LogWarning("No eventHandler attached"); }
            if (collisionHandler == null) { Debug.LogWarning("No CollisionHandler attached"); }

            //Check setup modules
            if (initialOffset == null) { Debug.LogWarning("No InitialOffset attached"); }

            //Check state modules
            if (statesArray.EmptyOrAllNull()) { Debug.LogWarning("No state found"); }


            for (statesIndex = 0; statesIndex < statesArray.Length; statesIndex++)
            {
                if (statesArray[statesIndex] != null)
                {
                    for (modulesIndex = 0; modulesIndex < statesArray[statesIndex].translateModules.Length; modulesIndex++)
                    {
                        if (statesArray[statesIndex].translateModules[modulesIndex] == null) { Debug.LogWarning("Missing module at: " + statesArray[statesIndex].stateName); };
                    }

                    for (modulesIndex = 0; modulesIndex < statesArray[statesIndex].rotateModules.Length; modulesIndex++)
                    {
                        if (statesArray[statesIndex].rotateModules[modulesIndex] == null) { Debug.LogWarning("Missing module at: " + statesArray[statesIndex].stateName); };
                    }
                }
            }
        }


        /// <summary>
        /// Makes all the needed initializations, such as passing this script to all the objects that needs it,
        /// and calls for every StartModule() method in each object (if applicable)
        /// </summary>
        private void StartModules()
        {
            if (eventHandler != null)
            {
                eventHandler.cameraController = this;
                eventHandler.StartModule();
            }

            if (initialOffset != null && initialOffset.enableModule)
            {
                initialOffset.cameraController = this;
                initialOffset.StartModule();
            }

            if (collisionHandler != null)
            {
                collisionHandler.cameraController = this;
                collisionHandler.StartModule();
            }

            for (statesIndex = 0; statesIndex < statesArray.Length; statesIndex++)
            {
                if (statesArray[statesIndex] != null)
                {
                    statesArray[statesIndex].cameraController = this;
                    for (modulesIndex = 0; modulesIndex < statesArray[statesIndex].translateModules.Length; modulesIndex++)
                    {
                        if (statesArray[statesIndex].translateModules[modulesIndex] != null)
                        {
                            statesArray[statesIndex].translateModules[modulesIndex].cameraController = this;
                            statesArray[statesIndex].translateModules[modulesIndex].StartModule();
                        }
                    }

                    for (modulesIndex = 0; modulesIndex < statesArray[statesIndex].rotateModules.Length; modulesIndex++)
                    {
                        if (statesArray[statesIndex].rotateModules[modulesIndex] != null)
                        {
                            statesArray[statesIndex].rotateModules[modulesIndex].cameraController = this;
                            statesArray[statesIndex].rotateModules[modulesIndex].StartModule();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs all the modules that it can find at the current state.
        /// </summary>
        /// <param name="currentStateNumber"></param>
        private void RunModules(int currentStateNumber)
        {
            #region TRANSLATE MODULES
            deltaTranslateVector = Vector3.zero;

            for (modulesIndex = 0; modulesIndex < statesArray[currentStateNumber].translateModules.Length; modulesIndex++)
            {
                if (statesArray[currentStateNumber].translateModules[modulesIndex] != null && statesArray[currentStateNumber].translateModules[modulesIndex].IsEnabled())
                {
                    statesArray[currentStateNumber].translateModules[modulesIndex].RunModule();
                    UpdateDeltaTranslateVector(statesArray[currentStateNumber].translateModules[modulesIndex]);
                }
            }
            if (collisionHandler == null)
            {
                transform.position += deltaTranslateVector;
            }
            else
            {
                transform.position = collisionHandler.CollisionTreatment(transform.position, deltaTranslateVector);
            }
            #endregion

            #region ROTATE MODULES

            for (modulesIndex = 0; modulesIndex < statesArray[currentStateNumber].rotateModules.Length; modulesIndex++)
            {
                if (statesArray[currentStateNumber].rotateModules[modulesIndex] != null && statesArray[currentStateNumber].rotateModules[modulesIndex].IsEnabled())
                {
                    statesArray[currentStateNumber].rotateModules[modulesIndex].RunModule();
                }
            }


            #endregion

            deltaTranslateVector = Vector3.zero;
        }

        /// <summary>
        /// This method is used to separate the translation movement at the 3 axis.
        /// </summary>
        /// <param name="module"></param>
        private void UpdateDeltaTranslateVector(TranslateModule module)
        {
            if (module.applyToX) { deltaTranslateVector.x += module.TranslateOutput.x; };
            if (module.applyToY) { deltaTranslateVector.y += module.TranslateOutput.y; };
            if (module.applyToZ) { deltaTranslateVector.z += module.TranslateOutput.z; };
        }

        /// <summary>
        /// Traces debuging drawings.
        /// </summary>
        public void TraceDebugTrail()
        {
            if (Trail)
            {
                if (lastPosition != null && lastPosition != Vector3.zero)
                {
                    Debug.DrawLine(lastPosition, transform.position, Color.green, 1000);
                }
                lastPosition = transform.position;
            }

            if (ViewDirection)
            {
                Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.blue, 1000);
            }

            if (CollisionPoints && collisionHandler != null)
            {
                for (int i = 0; i < collisionHandler.collisionPoints.Length; i++)
                {
                    Debug.DrawLine(transform.position, transform.position + collisionHandler.collisionPoints[i], Color.red, 1000);
                }
            }

            if (PrintState)
            {
                Debug.Log(cameraState);
            }
        }

        /// <summary>
        /// Sets the states for the Update and executes the modules of the current state
        /// </summary>
        /// <param name="currentUpdateMode"></param>
        /// <param name="testedUpdateMode"></param>
        /// <param name="currentCameraState"></param>
        public void ExecuteCameraUpdate()
        {
            eventHandler.SetCameraState();
            RunModules(currentStateIndex);
            TraceDebugTrail();
        }

        /// <summary>
        /// Returns the index for the state paramenter in the stateIndex.
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public void GetStateNumber()
        {
            for (int index = 0; index < statesArray.Length; index++)
            {
                if (statesArray[index].stateName == cameraState)
                {
                    currentStateIndex = index;
                }
            }
            Debug.LogError(this.ToString() + " - Current camera state could not found a match in the States Array." +
                "Please, check the stateName variable in the state script, and the enum scrip CameraStates." +
                "Current camera state:" + cameraState.ToString());
        }

        /// <summary>
        /// This method makes sures that we get the correct .deltaTime regardless which UpdateMode it is on.
        /// </summary>
        /// <returns></returns>
        public float DeltaTime()
        {
            if (updateMode == UpdateMode.FixedUpdate || updateMode == UpdateMode.LateUpdate)
            {
                return Time.fixedDeltaTime;
            }
            else
            {
                return Time.deltaTime;
            }
        }
    }
}
