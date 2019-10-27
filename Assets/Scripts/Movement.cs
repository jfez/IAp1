using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float NORMALSPEED = 100f;
    const float RUNNINGSPEED = 150f;
    const float STEALTHSPEED = 50f;

    public GameObject baliza;

    public bool detected = false;
    

    [HideInInspector]public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody rb2d;
    private Vector2 moveVelocity;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    private Vector3 moveFor;
    private Vector3 moveSide;
    private int floorMask;
    private float dashForce;
    [HideInInspector] public float timerDash;
    [HideInInspector] public float dashCD;

    [HideInInspector] public float timerBaliza;
    [HideInInspector] public float balizaCD;

    [HideInInspector] public bool balizaON;
    [HideInInspector] public bool exit;

    private GameObject balizaInstantiated;

    private ExitManager exitManager;

    public GameObject shield;
    [HideInInspector] public bool growingShield;

    [HideInInspector] public float timerShield;
    [HideInInspector] public float shieldCD;
    private float growingSpeed;
    [HideInInspector] public bool shieldActive;



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
        dashForce = 7500f;
        dashCD = 3f;
        timerDash = dashCD;
        balizaCD = 15f;
        timerBaliza = balizaCD;
        shieldCD = 10f;
        timerShield = shieldCD;
        balizaON = false;
        exitManager = GameObject.FindGameObjectWithTag("ExitManager").GetComponent<ExitManager>();
        exit = false;
        shield.SetActive(false);
        growingShield = false;
        growingSpeed = 10f;
        shieldActive = false;
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

        if(Input.GetKeyDown(KeyCode.Space) && timerDash > dashCD && !exitManager.pause){
            rb2d.AddForce(transform.up*dashForce);
            timerDash = 0f;   
        }

        if(Input.GetKeyDown(KeyCode.E) && timerBaliza > balizaCD && !exitManager.pause && !detected){
            
            if(!balizaON){
                balizaInstantiated = Instantiate(baliza, transform.position, Quaternion.identity);
                balizaON = true;   

            }

            else{
                if (balizaInstantiated != null) {
                    transform.position = balizaInstantiated.transform.position;
                    Destroy(balizaInstantiated);
                    balizaON = false;
                    timerBaliza = 0f;
                }
            }
            
        }

        if(Input.GetKeyDown(KeyCode.R) && timerShield > shieldCD && !exitManager.pause && !growingShield &&!shieldActive){
            shield.transform.localScale = new Vector3(0,0,0);
            growingShield=true;
            shield.SetActive(true);
               
            
        }

        if (growingShield){
            if(shield.transform.localScale.x < 40){
                shield.transform.localScale += new Vector3(1,1,1) * Time.deltaTime * growingSpeed;
                

            }

            else{
                growingShield = false;
                shieldActive = true;
            }
        }


        //Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput.normalized * speed;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        timerDash += Time.deltaTime;
        timerBaliza += Time.deltaTime;
        timerShield += Time.deltaTime;

        

        
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
