using System.Collections;
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

        if (aStar.timeElapsedSinceLastSearch >= .25f)
        {
            aStar.StartCoroutine(aStar.SearchPath(controller.chaseTarget));
            aStar.timeElapsedSinceLastSearch = 0;
        }
    }
}