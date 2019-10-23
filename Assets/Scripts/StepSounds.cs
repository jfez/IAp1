using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    private Movement movement;
    private float dstToTarget;
    private Transform target;
    private float timer;
    private float maxTime;
    private float randomNumber;
    private float secureDistance;
    private float added;
    private float addedSpeed;

    //15 is a good secure distance
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        maxTime = 1.5f;
        timer = maxTime;
        secureDistance = 15f;
        added = 0.1f;
        addedSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
         timer += Time.deltaTime;

         if (timer >= maxTime){
            timer = 0f;
            dstToTarget = Vector3.Distance(transform.position, target.position);

            if(dstToTarget < secureDistance){
                randomNumber = Random.Range(0.0f, 1.0f); 
                
                float prob = 1 - (dstToTarget/secureDistance) + added;

                if(movement.speed == 100f){
                    addedSpeed = 0.1f;

                }
                else if (movement.speed == 150f){
                    addedSpeed = 0.25f;

                }

                else if (movement.speed == 50f){
                    addedSpeed = -0.4f;

                }

                if (movement.horizontal == 0f && movement.vertical == 0f){
                    //If the player is not moving, they can't hear him
                    addedSpeed = -prob;
                }
                if(randomNumber < prob + addedSpeed){
                    print("ALERTA");

                }
                print("La probabilidad es de: " + prob + "+" + addedSpeed + " y el número aleatorio es: " + randomNumber);
            }
            

         }
         
         
    }
}
