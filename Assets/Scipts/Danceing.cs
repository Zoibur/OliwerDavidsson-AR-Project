using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


public class Danceing : MonoBehaviour
{
    [SerializeField] private RoamingNPC player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private List<string> animations;
    
    public void StartDanceing()
    {
        player.agent.SetDestination(target.position);
        
        StartCoroutine(gameManager.WaitForAgentToReachDestination(() =>
        {
            var rand = new Random();
            int number = rand.Next(0, animations.Count-1);
           
            var startPosition = player.transform.position;
            animator.SetBool(animations[number], true);
            player.active = false;
            player.agent.enabled = false;
            cooldown.InactiveButton();
            StartCoroutine(gameManager.ResetState(6.0f, () =>
            {
                animator.SetBool(animations[number], false);

                player.agent.enabled = true;
                player.active = true;
                
                player.transform.position = startPosition;
                cooldown.ActiveButton();
            }));
            
        }));
    }
}
