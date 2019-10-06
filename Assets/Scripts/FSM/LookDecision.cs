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
        RaycastHit2D hit = Physics2D.CircleCast(controller.eyes.position, controller.enemyStats.lookRange, controller.eyes.forward);
        if (hit != false && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Jugador avistado");
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}