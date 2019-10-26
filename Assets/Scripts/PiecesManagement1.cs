using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManagement1 : MonoBehaviour
{
    [HideInInspector] public int piecesCollected;
    private Movement movement;
    
    // Start is called before the first frame update
    void Start()
    {
        piecesCollected = 0;
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (piecesCollected >= 3){
            movement.exit = true;

        }
    }
}
