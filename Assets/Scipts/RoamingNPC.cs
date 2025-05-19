using UnityEngine;
using UnityEngine.AI;

public class RoamingNPC : MonoBehaviour
{
    public enum State
    {
        Idle,
        Swinging,
        Eating,
        Sleeping,
    }
    [SerializeField] private Animator animator;
    public State state = State.Idle;
    public float roamRadius = 1.5f;
    public float waitTime = 2f;
    public float agentSpeed = 0.5f; // Adjustable speed
    public bool active;

    public NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent.speed = agentSpeed;
        timer = waitTime;
        MoveToNewDestination();
    }

    void Update()
    {
        if (!active){return;}
        timer += Time.deltaTime;
     
        
            if (timer >= waitTime)
            {
                MoveToNewDestination();
                timer = 0f;
                
            }
        
    }

    void MoveToNewDestination()
    {
        
        Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
        agent.SetDestination(newPos);
        animator.SetTrigger("Walking");
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
