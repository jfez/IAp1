using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Arrival")]
public class ArrivalDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool hasArrived = Arrival(controller);
        return hasArrived;
    }

    private bool Arrival(StateController controller)
    {
        if (Vector2.Distance(controller.eyes.position, controller.wayPointList[controller.nextWayPoint].position) < 2)
        {
            return true;
        }
        return false;
    }
}
