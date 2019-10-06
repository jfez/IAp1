using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Debug.Log("Apatrullando la ciudad...");

        Vector3 targetPoint = controller.wayPointList[controller.nextWayPoint].position;
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, targetPoint, controller.enemyStats.moveSpeed * Time.deltaTime);
        controller.transform.up = targetPoint - controller.transform.position;

        if (Vector2.Distance(controller.transform.position, targetPoint) < 0.5)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}