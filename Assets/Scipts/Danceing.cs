using UnityEngine;
using UnityEngine.AI;



public class Danceing : MonoBehaviour
{
    [SerializeField] private RoamingNPC player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Cooldown cooldown;
    public void StartDanceing()
    {
        player.agent.SetDestination(target.position);
        
        StartCoroutine(gameManager.WaitForAgentToReachDestination(() =>
        {
            
            var startPosition = player.transform.position;
            animator.SetBool("Dans", true);
            player.active = false;
            player.agent.enabled = false;
            cooldown.InactiveButton();
            StartCoroutine(gameManager.ResetState(6.0f, () =>
            {
                animator.SetBool("Dans", false);

                player.agent.enabled = true;
                player.active = true;
                
                player.transform.position = startPosition;
                cooldown.ActiveButton();
            }));
            
        }));
    }
}
