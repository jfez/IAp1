using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollected2 : MonoBehaviour
{
    private Movement movement;
    
    // Start is called before the first frame update
    void Start()
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
