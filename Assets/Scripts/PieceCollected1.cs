using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollected1 : MonoBehaviour
{
    private PiecesManagement1 piecesManagement1;

    private void Awake()
    {
        piecesManagement1 = GameObject.FindGameObjectWithTag("PiecesManager").GetComponent<PiecesManagement1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            piecesManagement1.piecesCollected++;
            Destroy(gameObject);

        }
    }
}
