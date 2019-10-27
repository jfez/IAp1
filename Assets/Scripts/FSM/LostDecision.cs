using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Lost")]
public class LostDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetLost = Lost(controller);
        return targetLost;
    }

    private bool Lost(StateController controller)
    {
        if (controller.fieldOfView.visibleTargets.Count == 0) {
            if (!controller.timing){
                controller.StartTiming();
            }
            else {
                //Time after lost the target that the enemy waits to return to his patrol
                if (controller.timer > 3){
                    controller.StopTiming();
                    controller.chaseTarget = null;
                    controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.wayPointList[controller.nextWayPoint]));
                    controller.exclamation.SetActive(false);
                    controller.interrogation.SetActive(false);
                    controller.soundListener.movement.detected = false;
                    //controller.speed = 2f;
                    return true;
                }
            }     
        }
        return false;
    }
}