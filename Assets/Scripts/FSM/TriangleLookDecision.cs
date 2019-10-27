using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/TriangleLook")]
public class TriangleLookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.fieldOfView.visibleTargets.Count != 0)
        {
            if (controller.wanderer)
            {
                StateController[] stateControllers = FindObjectsOfType<StateController>();
                foreach(StateController sC in stateControllers)
                {
                    if (sC != controller && sC.currentState != sC.chaseState)
                    {
                        sC.exclamation.SetActive(false);
                        sC.interrogation.SetActive(true);
                        sC.warnedPoint = controller.fieldOfView.visibleTargets[0].position;
                        sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(controller.fieldOfView.visibleTargets[0]));
                        sC.TransitionToState(controller.alertState);
                    }
                    else
                    {
                        sC.interrogation.SetActive(false);
                        sC.exclamation.SetActive(true);
                        controller.chaseSound.Play();
                        sC.soundListener.movement.detected = true;
                        sC.warnedPoint = controller.piecesWardPoint.position;
                        sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(controller.piecesWardPoint));
                        //Debug.Log("eh");
                    }
                }
                return true;
            }
        }
        return false;
    }
}