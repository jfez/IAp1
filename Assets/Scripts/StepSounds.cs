using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [HideInInspector] public Movement movement;
    [HideInInspector] public Transform target;
    [HideInInspector] public float timer;
    [HideInInspector] public float maxTime;
    [HideInInspector] public float secureDistance;
    [HideInInspector] public float added;

    //15 is a good secure distance

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTime = 1.5f;
        timer = maxTime;
        secureDistance = 15f;
        added = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
         timer += Time.deltaTime;    
    }
}
