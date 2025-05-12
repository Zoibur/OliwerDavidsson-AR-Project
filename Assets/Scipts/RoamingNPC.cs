using UnityEngine;
using UnityEngine.AI;

public class RoamingNPC : MonoBehaviour
{
    public float roamRadius = 1.5f;
    public float waitTime = 2f;
    public float agentSpeed = 0.5f; // Adjustable speed

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = agentSpeed;
        timer = waitTime;
        MoveToNewDestination();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (timer >= waitTime)
            {
                MoveToNewDestination();
                timer = 0f;
            }
        }
    }

    void MoveToNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, roamRadius, -1);
        agent.SetDestination(newPos);
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
