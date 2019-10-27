using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.fieldOfView.visibleTargets.Count != 0) {
            if (!controller.wanderer) {
                controller.chaseTarget = controller.fieldOfView.visibleTargets[0].transform;
                controller.soundListener.movement.detected = true;
                controller.interrogation.SetActive(false);
                controller.exclamation.SetActive(true);
                controller.chaseSound.Play();
                //controller.speed = 14f;
                return true;
            }
            else
            {
                StateController[] stateControllers = controller.allStateControllers;
                foreach (StateController sC in stateControllers)
                {
                    if (!sC.wanderer && sC.currentState != sC.alertState && sC.currentState != sC.chaseState && controller.currentState != controller.scanState)
                    {
                        sC.exclamation.SetActive(false);
                        sC.interrogation.SetActive(true);
                        sC.warnedPoint = controller.fieldOfView.visibleTargets[0].position;
                        sC.TransitionToState(sC.alertState);
                        sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(controller.fieldOfView.visibleTargets[0]));
                    }
                    else if (sC.currentState != sC.protectPiecesState)
                    {
                        if (Vector2.Distance(sC.eyes.position, sC.piecesWardPoint.position) < 1f) continue;
                        sC.interrogation.SetActive(false);
                        sC.exclamation.SetActive(true);
                        sC.soundListener.movement.detected = true;
                        sC.warnedPoint = sC.piecesWardPoint.position;
                        sC.aStarUnit.StartCoroutine(sC.aStarUnit.SearchPath(sC.piecesWardPoint));
                        sC.TransitionToState(sC.protectPiecesState);
                        for (int i = 0; i < 3; i++)
                        {
                            if (sC.piecesCollider[i] != null) sC.piecesCollider[i].enabled = false;
                        }
                    }
                }
            }
        }
        return false;
    }
}