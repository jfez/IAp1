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
        if (Vector2.Distance(controller.transform.position, controller.chaseTarget.position) > controller.enemyStats.lookRange)
        {
            Debug.Log("Jugador perdido");
            controller.chaseTarget = null;
            return true;
        }
        else
        {
            return false;
        }
    }
}