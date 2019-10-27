using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/LookMarker")]
public class LookMarkerDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool markerVisible = MarkerSeen(controller);
        return markerVisible;
    }

    private bool MarkerSeen(StateController controller)
    {
        if (controller.fieldOfView.visibleBalizas.Count != 0)
        {
            if (controller.fieldOfView.visibleBalizas[0] != null)
            {
                controller.markerPoint = controller.fieldOfView.visibleBalizas[0].position;
                controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.fieldOfView.visibleBalizas[0]));
                return true;
            }
        }
        return false;
    }
}