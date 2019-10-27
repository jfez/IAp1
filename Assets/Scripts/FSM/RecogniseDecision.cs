using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Recognise")]
public class RecogniseDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool playerRecognised = Recognise(controller);
        return playerRecognised;
    }

    private bool Recognise(StateController controller)
    {
        controller.transform.Rotate(0, 0, controller.enemyStats.searchingTurnSpeed * Time.deltaTime * 10f);
        if (controller.CheckIfCountDownElapsed(controller.enemyStats.searchDuration))
        {
            controller.interrogation.SetActive(false);
            controller.exclamation.SetActive(false);
            controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.wayPointList[controller.nextWayPoint]));
            if (controller.wanderer) {
                controller.soundListener.movement.detected = false;
                for (int i = 0; i < 3; i++)
                {
                    if (controller.piecesCollider[i] != null) controller.piecesCollider[i].enabled = true;
                }
            }
            return true;
        }
        return false;
    }
}
