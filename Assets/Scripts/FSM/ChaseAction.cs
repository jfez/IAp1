using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        Debug.Log("Persiguiendo al jugador...");

        Vector3 targetPoint = controller.chaseTarget.position;
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, targetPoint, controller.enemyStats.moveSpeed * Time.deltaTime);
        controller.transform.up = targetPoint - controller.transform.position;
    }
}