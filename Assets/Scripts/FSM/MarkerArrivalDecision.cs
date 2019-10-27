using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/MarkerArrival")]
public class MarkerArrivalDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool markerTouched = MarkerTouched(controller);
        return markerTouched;
    }

    private bool MarkerTouched(StateController controller)
    {
        Collider[] colliders = Physics.OverlapSphere(controller.eyes.position, controller.sphereCollider.radius, controller.fieldOfView.balizaMask);

        if (colliders.Length != 0)
        {
            controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.wayPointList[controller.nextWayPoint]));
            controller.soundListener.movement.balizaON = false;
            controller.soundListener.movement.timerBaliza = 0f;
            Destroy(colliders[0].gameObject);
            return true;
        }
        return false;
    }
}
