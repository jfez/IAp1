using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float NORMALSPEED = 4f;
    const float RUNNINGSPEED = 8f;
    const float STEALTHSPEED = 2f;

    private float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private Vector2 moveVelocity;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        speed = NORMALSPEED;
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = RUNNINGSPEED;
        }

        else if (!Input.GetKey(KeyCode.LeftShift) && speed == RUNNINGSPEED)
        {
            speed = NORMALSPEED;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = STEALTHSPEED;
        }

        else if (!Input.GetKey(KeyCode.LeftControl) && speed == STEALTHSPEED)
        {
            speed = NORMALSPEED;
        }

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }
    
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}
