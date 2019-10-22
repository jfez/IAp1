﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public EnemyStats enemyStats;
    public Transform eyes;
    public State remainState;
    public List<Transform> wayPointList;

    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Unit aStarUnit;

    [HideInInspector] public FieldOfView fieldOfView;

    [HideInInspector] public float timer;
    [HideInInspector] public bool timing;

    private bool aiActive;
    

    void Awake()
    {
        aStarUnit = GetComponent<Unit>();
        fieldOfView = GetComponent<FieldOfView>();
        timing = false;
        timer = 0f;
    }

    void Start()
    {
        aiActive = true;
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
        if (timing){
            Clock();
        }
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookRange);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    private void Clock(){
        timer += Time.deltaTime;
    }

    public void StartTiming(){
        timer = 0f;
        timing = true;
    }

    public void StopTiming(){
        timing = false;
    }
}