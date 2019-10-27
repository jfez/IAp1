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
        if (controller.aStarUnit.timeElapsedSinceLastSearch >= 2f)        //.25f
        {
            controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.chaseTarget));
            controller.aStarUnit.timeElapsedSinceLastSearch = 0;
            
        }
    }
}