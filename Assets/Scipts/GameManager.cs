using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Swing swing;
    [SerializeField] private RoamingNPC player;
    [SerializeField] private Transform seat;
    
    public void StartSwinging()
    {
        if (player.state == RoamingNPC.State.Swinging) return;
        player.state = RoamingNPC.State.Swinging;
        player.agent.SetDestination(seat.position);
        StartCoroutine(WaitForAgentToReachDestination(() =>
        {
            var startPosition = player.transform.position;
            swing.StartSwing();
            player.transform.SetParent(swing.swingSeat);
            player.transform.localPosition = Vector3.zero;
            player.active = false;
            player.agent.enabled = false;
            StartCoroutine(ResetState(5.0f, () =>
            {
                player.agent.enabled = true;
                player.active = true;
                swing.StopSwing();
                player.transform.SetParent(null);
                player.transform.position = startPosition;
            }));
        }));
    }

    public IEnumerator ResetState(float time, Action onComplete = null)
    {
        yield return new WaitForSeconds(time);
        player.state = RoamingNPC.State.Idle;
        onComplete?.Invoke();
    }

    private IEnumerator WaitForAgentToReachDestination(Action onComplete)
    {
        Debug.LogError($"remaining distance {player.agent.remainingDistance}");
        Debug.LogError($"remaining distance {player.agent.stoppingDistance}");
        yield return new WaitUntil(() => player.agent.remainingDistance <= player.agent.stoppingDistance);
        onComplete?.Invoke();
    }
}
