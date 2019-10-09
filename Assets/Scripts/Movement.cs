using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float NORMALSPEED = 70f;
    const float RUNNINGSPEED = 100f;
    const float STEALTHSPEED = 30f;
    

    private float speed;             //Floating point variable to store the player's movement speed.
    //private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private Rigidbody rb2d;
    private Vector2 moveVelocity;
    private float horizontal;
    private float vertical;
    private Vector3 moveFor;
    private Vector3 moveSide;
    private int floorMask;


    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        //rb2d = GetComponent<Rigidbody2D>();
        rb2d = GetComponent<Rigidbody>();
        speed = NORMALSPEED;
        horizontal = 0;
        vertical = 0;
        floorMask = LayerMask.GetMask("Floor");
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


        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput.normalized * speed;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        

        
    }
    
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(horizontal, vertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement.normalized * speed);

        Turning();
        
    }

    void Turning()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);

        transform.up = direction;
    }


}
