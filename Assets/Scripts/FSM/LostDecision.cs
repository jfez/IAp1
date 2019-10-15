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
        Collider[] colliders = Physics.OverlapSphere(controller.eyes.position, controller.enemyStats.lookRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return false;
            }
        }
        controller.chaseTarget = null;
        return true;
    }
}