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
            controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.wayPointList[controller.nextWayPoint]));
            return true;
        }
        return false;
    }
}
