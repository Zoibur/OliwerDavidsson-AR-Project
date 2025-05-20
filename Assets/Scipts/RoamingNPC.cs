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
            if (isDoingAction)
            {
                StartCoroutine(PerformAction());
                isDoingAction = false; // Prevent repeating
                animator.SetTrigger("Squat");
                return;
            } 
            
            if (isDoingEating)
            {
                StartCoroutine(PerformEating());
                isDoingEating = false; // Prevent repeating
                animator.SetTrigger("Eat");
                return;
            }
            
            if (isDoingDancing)
            {
                StartCoroutine(PerformDancing());
                isDoingDancing = false; // Prevent repeating
                animator.SetTrigger("Dance");
                return;
            }
            
            if (!isWaiting)
            {
                isWaiting = true;
                waitTimer = waitTime;
                animator.SetTrigger("Idle");

            }

            // Wait at destination before moving again
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                MoveToNewDestination();
                
            }

        }

       
    }
    public IEnumerator ResetState(float time, Action onComplete = null)
    {
        yield return new WaitForSeconds(time);
        player.state = RoamingNPC.State.Idle;
        onComplete?.Invoke();
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
    public void WalkToTargetAndAct(Transform target)
    {
        isDoingAction = true;
        actionTarget = target;
        

        agent.SetDestination(actionTarget.position);
    }
    public void WalkToTargetAndEat(Transform target)
    {
        isDoingEating = true;
        actionTarget = target;
        

        agent.SetDestination(eatingTarget.position);
    }
    public void WalkToTargetAndDance(Transform target)
    {
        isDoingDancing = true;
        actionTarget = target;
        

        agent.SetDestination(danceTarget.position);
    }
    
    private IEnumerator PerformAction()
    {
        if (actionTarget != null)
        {
            Vector3 lookDir = (actionTarget.position - transform.position).normalized;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        
        
        player.active = false;
        player.agent.enabled = false;
        yield return new WaitForSeconds(8);
        player.active = true;
        player.agent.enabled = true;
        
        MoveToNewDestination();
    }
    
    private IEnumerator PerformEating()
    {
        if (actionTarget != null)
        {
            Vector3 lookDir = (actionTarget.position - transform.position).normalized;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        
        player.active = false;
        player.agent.enabled = false;
        yield return new WaitForSeconds(8);
        player.active = true;
        player.agent.enabled = true;
        MoveToNewDestination();
    }
    
    private IEnumerator PerformDancing()
    {
        if (actionTarget != null)
        {
            Vector3 lookDir = (actionTarget.position - transform.position).normalized;
            lookDir.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        
        player.active = false;
        player.agent.enabled = false;
        yield return new WaitForSeconds(8);
        player.active = true;
        player.agent.enabled = true;
        MoveToNewDestination();
    }

    
}
