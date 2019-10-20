using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    Vector2 velocity = Vector2.zero;

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Vector3 velocity = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 direction = Vector3.SmoothDamp(controller.transform.up.normalized, (controller.wayPointList[controller.nextWayPoint].position - controller.transform.position).normalized, ref velocity, Time.deltaTime * controller.enemyStats.searchingTurnSpeed);

        float speedPercent = Mathf.Clamp01((controller.wayPointList[controller.nextWayPoint].position - controller.transform.position).magnitude / controller.enemyStats.attackRange);
        controller.transform.up = new Vector3(direction.x, direction.y, 0f);
        controller.transform.Translate(Vector3.up * Time.deltaTime * controller.enemyStats.moveSpeed * speedPercent, Space.Self);

        if (Vector2.Distance(controller.eyes.position, controller.wayPointList[controller.nextWayPoint].position) < 0.5)
        {
            controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        }
    }
}