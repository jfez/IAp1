using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    Vector2 velocity = Vector2.zero;
    private float timer;
    

    

    public override void Act(StateController controller)
    {
        Patrol(controller);
        
    }

    private void Patrol(StateController controller)
    {
        timer += Time.deltaTime;
        
        Vector3 velocity = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 direction = Vector3.SmoothDamp(controller.transform.up.normalized, (controller.wayPointList[controller.nextWayPoint].position - controller.transform.position).normalized, ref velocity, Time.deltaTime * controller.enemyStats.searchingTurnSpeed);

        float speedPercent = Mathf.Clamp01((controller.wayPointList[controller.nextWayPoint].position - controller.transform.position).magnitude / controller.enemyStats.attackRange);
        controller.transform.up = new Vector3(direction.x, direction.y, 0f);
        controller.transform.Translate(Vector3.up * Time.deltaTime * controller.enemyStats.moveSpeed * speedPercent, Space.Self);

        if (Vector2.Distance(controller.eyes.position, controller.wayPointList[controller.nextWayPoint].position) < 0.5 && timer >= 3)
        {
            
            timer = 0f;
            //controller.enemyStats.moveSpeed = 1.5f;
            float rnd = Random.Range(0.0f, 1.0f);
            //Debug.Log(rnd);
            if (rnd < 0.7f){
                //controller.enemyStats.moveSpeed = 1.5f;
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
                Debug.Log("sigo");

            }

            else{
                //WATCH OUT because it changes in the Scriptable Object
                //controller.enemyStats.moveSpeed = 0;
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
                Debug.Log("paro");


            }
            
        }
    }
}