using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RoamingNPC : MonoBehaviour
{
    public enum State
    {
        Idle,
        Swinging,
        Eating,
        Sleeping,
    }
    public State state = State.Idle;
    public float roamRadius = 1.5f;
    public float waitTimer = 0f;
    public float waitTime = 2f;
    private bool isWaiting = false;
    public bool active;
    [SerializeField]Animator animator;
    
    private bool isDoingAction;
    private bool isDoingEating;
    private bool isDoingDancing;
    [SerializeField]private Transform actionTarget;
    [SerializeField]private Transform eatingTarget;
    [SerializeField]private Transform danceTarget;
    
    [SerializeField] private RoamingNPC player;

    public NavMeshAgent agent;
    private float timer;

    void Start()
    {

        MoveToNewDestination();
        
        
    }

    void Update()
    {
        if (!active)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Wait at destination before moving again
            waitTimer -= Time.deltaTime;
            
            if (!isWaiting)
            {
                isWaiting = true;
                waitTimer = waitTime;
                

            }
            
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                MoveToNewDestination();
                
            }

        }

       
    }
   
    void MoveToNewDestination()
    {

        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            
        }

    }

    
    

    
}
