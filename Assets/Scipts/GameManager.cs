using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Swing swing;
    [SerializeField] private RoamingNPC player;
    [SerializeField] private Transform seat;
    [SerializeField] private Animator npcAnimator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform faceForwardTransform;
    [SerializeField] private Cooldown cooldown;

    private void Update()
    {
        bool isMoving = agent.velocity.magnitude > 0.01f;
        
        npcAnimator.SetBool("Move", isMoving);
    }

    public void StartSwinging()
    {
        if (player.state == RoamingNPC.State.Swinging) return;
        player.state = RoamingNPC.State.Swinging;
        player.agent.SetDestination(seat.position);
        StartCoroutine(WaitForAgentToReachDestination(() =>
        {
            var startPosition = player.transform.position;
            FaceDirection(faceForwardTransform.position);
            npcAnimator.SetBool("Sit", true);
            swing.StartSwing();
            player.transform.SetParent(swing.swingSeat);
            player.transform.localPosition = Vector3.zero;
            player.active = false;
            player.agent.enabled = false;
            cooldown.InactiveButton();
            StartCoroutine(ResetState(6.0f, () =>
            {
                npcAnimator.SetBool("Sit", false);
                player.agent.enabled = true;
                player.active = true;
                swing.StopSwing();
                player.transform.SetParent(null);
                player.transform.position = startPosition;
                cooldown.ActiveButton();
            }));
        }));
    }

    public IEnumerator ResetState(float time, Action onComplete = null)
    {
        yield return new WaitForSeconds(time);
        player.state = RoamingNPC.State.Idle;
        onComplete?.Invoke();
    }

    public IEnumerator WaitForAgentToReachDestination(Action onComplete)
    {
        while (player.agent.pathPending || player.agent.remainingDistance > player.agent.stoppingDistance)
        {
            
            yield return null;
        }
        onComplete?.Invoke();
    }
    private void FaceDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - player.transform.position).normalized;
        direction.y = 0; // Ignore vertical rotation

        if (direction != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
