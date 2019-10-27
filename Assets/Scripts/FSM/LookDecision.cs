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
            if (!controller.wanderer) {
                controller.chaseTarget = controller.fieldOfView.visibleTargets[0].transform;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().detected = true;
                controller.interrogation.SetActive(false);
                controller.exclamation.SetActive(true);
                controller.chaseSound.Play();
                //controller.speed = 14f;
                return true;
            }   
        }
        return false;
    }
}