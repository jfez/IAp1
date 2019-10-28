using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    public State triangleAlertState;

    private FieldOfView fov;
    private bool playerInRange = false;

    private StateController[] stateControllers;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
        stateControllers = FindObjectsOfType<StateController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.visibleTargets.Count != 0 && !playerInRange)
        {
            playerInRange = true;
            
            /*foreach (StateController sC in stateControllers)
            {
                if (!sC.wanderer && sC.currentState != sC.chaseState)
                {
                    sC.exclamation.SetActive(false);
                    sC.interrogation.SetActive(true);
                    sC.warnedPoint = fov.visibleTargets[0].position;
                    sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(fov.visibleTargets[0]));
                    sC.TransitionToState(triangleAlertState);
                }
            }*/
            if (stateControllers[2].currentState != stateControllers[2].chaseState){
                    stateControllers[2].exclamation.SetActive(false);
                    stateControllers[2].interrogation.SetActive(true);
                    stateControllers[2].warnedPoint = fov.visibleTargets[0].position;
                    stateControllers[2].aStarUnit.StartCoroutine(stateControllers[2].aStarUnit.SearchPath(fov.visibleTargets[0]));
                    stateControllers[2].TransitionToState(triangleAlertState);
                    Debug.Log("Cuadrado a la cámara");

            }
        } 
        else if (playerInRange && fov.visibleTargets.Count == 0){
            playerInRange = false;
        }
    }
}
