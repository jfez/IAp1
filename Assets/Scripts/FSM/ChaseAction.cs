﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    private Unit aStar = null;

    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        if (aStar == null) aStar = controller.GetComponent<Unit>();

        if (aStar.timeElapsedSinceLastSearch >= 0.5f)
        {
            aStar.SearchPath(controller.chaseTarget);
            aStar.timeElapsedSinceLastSearch = 0;
        }

        /*Vector3 targetPoint = controller.chaseTarget.position;
        controller.transform.position = Vector2.MoveTowards(controller.transform.position, targetPoint, controller.enemyStats.moveSpeed * Time.deltaTime);
        controller.transform.up = targetPoint - controller.transform.position;*/
    }
}