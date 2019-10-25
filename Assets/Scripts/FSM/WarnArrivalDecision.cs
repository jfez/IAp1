using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/WarnArrival")]
public class WarnArrivalDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool hasArrived = WarnArrival(controller);
        return hasArrived;
    }

    private bool WarnArrival(StateController controller)
    {
        float distToWarnPoint = Vector2.Distance(controller.eyes.position, controller.warnedPoint);
        if (distToWarnPoint < 3f)
        {
            return true;
        }
        return false;
    }
}