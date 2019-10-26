using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{

    public int stairNum;

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

    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player"){
            
            if(stairNum == 0 && movement.exit){
                SceneManager.LoadScene("Stencil");

            }

            if(stairNum == 1 && movement.exit){
                SceneManager.LoadScene("Initial");

            }

            
            
        }
    }
}
