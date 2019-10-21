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
                controller.chaseTarget = controller.fieldOfView.visibleTargets[0].transform;
                return true;
        }
        return false;
        /*Collider[] colliders = Physics.OverlapSphere(controller.eyes.position, controller.enemyStats.lookRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                controller.chaseTarget = colliders[i].transform;
                return true;
            }
        }
        return false;*/
    }
}