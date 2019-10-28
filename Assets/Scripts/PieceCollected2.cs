using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollected2 : MonoBehaviour
{
    private Movement movement;

    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            movement.exit = true;
            Destroy(gameObject);

        }
    }
}
