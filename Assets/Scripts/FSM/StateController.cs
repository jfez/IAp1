using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State triangleAlertState;
    public State chaseState;
    public EnemyStats enemyStats;
    public GameObject bullet;
    public Transform eyes;
    public State remainState;
    public List<Transform> wayPointList;
    public Transform piecesWardPoint;

    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public Vector3 warnedPoint;
    [HideInInspector] public Vector3 markerPoint;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public Unit aStarUnit;

    [HideInInspector] public SphereCollider sphereCollider;
    [HideInInspector] public FieldOfView fieldOfView;
    [HideInInspector] public StepSounds soundListener;

    [HideInInspector] public float timer;
    [HideInInspector] public bool timing;
    [HideInInspector] public float speed;
    private Unit unit;

    private bool aiActive;

    public GameObject exclamation;
    public GameObject interrogation;

    [HideInInspector] public AudioSource chaseSound;

    [Header("Only for triangles")]
    public bool wanderer = false;

    void Awake()
    {
        aStarUnit = GetComponent<Unit>();
        sphereCollider = GetComponent<SphereCollider>();
        fieldOfView = GetComponent<FieldOfView>();
        soundListener = GetComponent<StepSounds>();
        timing = false;
        timer = 0f;
        unit = GetComponent<Unit>();
        speed = unit.speed;
        exclamation.SetActive(false);
        interrogation.SetActive(false);
        chaseSound = GameObject.FindGameObjectWithTag("ChaseSound").GetComponent<AudioSource>();
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