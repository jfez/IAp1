using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Listen")]
public class ListenDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetListened = Listen(controller);
        return targetListened;
    }

    private bool Listen(StateController controller)
    {
        if (controller.soundListener.timer >= controller.soundListener.maxTime)
        {
            controller.soundListener.timer = 0f;
            float distToTarget = Vector3.Distance(controller.eyes.position, controller.soundListener.target.position);
            float addedSpeed = 0f;

            if (distToTarget < controller.soundListener.secureDistance)
            {
                float randomNumber = Random.Range(0f, 1f);
                float prob = 1 - (distToTarget / controller.soundListener.secureDistance) + controller.soundListener.added;

                if (controller.soundListener.movement.speed == 100f)
                {
                    addedSpeed = 0.1f;

                }
                else if (controller.soundListener.movement.speed == 150f)
                {
                    addedSpeed = 0.25f;

                }

                else if (controller.soundListener.movement.speed == 50f)
                {
                    addedSpeed = -0.4f;

                }

                if (controller.soundListener.movement.horizontal == 0f && controller.soundListener.movement.vertical == 0f)
                {
                    //If the player is not moving, they can't hear him
                    addedSpeed = -prob;
                }
                if (randomNumber < prob + addedSpeed)
                {
                    controller.interrogation.SetActive(true);
                    controller.warnedPoint = controller.soundListener.target.position;
                    controller.aStarUnit.StartCoroutine(controller.aStarUnit.SearchPath(controller.soundListener.target));
                    return true;

                }
            }
        }
        return false;
    }
}
