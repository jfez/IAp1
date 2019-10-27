using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetector : MonoBehaviour
{
    public State triangleAlertState;

    private FieldOfView fov;
    private bool playerInRange = false;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fov.visibleTargets.Count != 0 && !playerInRange)
        {
            playerInRange = true;
            StateController[] stateControllers = FindObjectsOfType<StateController>();
            foreach (StateController sC in stateControllers)
            {
                if (!sC.wanderer && sC.currentState != sC.chaseState)
                {
                    sC.exclamation.SetActive(false);
                    sC.interrogation.SetActive(true);
                    sC.warnedPoint = fov.visibleTargets[0].position;
                    sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(fov.visibleTargets[0]));
                    sC.TransitionToState(triangleAlertState);
                }
            }
        }
        playerInRange = false;
    }
}
