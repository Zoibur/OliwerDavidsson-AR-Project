using UnityEngine;

public class Eating : MonoBehaviour
{
    [SerializeField] private RoamingNPC player;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject eatingPrefab;
    [SerializeField] private Cooldown cooldown;
    [SerializeField] private Transform targetFood;

    public void StartEating()
    {
        player.agent.SetDestination(target.position);
        
        StartCoroutine(gameManager.WaitForAgentToReachDestination(() =>
        {
            var startPosition = player.transform.position;
            GameObject food = Instantiate(eatingPrefab, targetFood.position, Quaternion.identity);
            animator.SetBool("Food", true);
            player.active = false;
            player.agent.enabled = false;
            cooldown.InactiveButton();
            StartCoroutine(gameManager.ResetState(6.0f, () =>
            {
                animator.SetBool("Food", false);

                player.agent.enabled = true;
                player.active = true;
                
                player.transform.position = startPosition;
                
                Destroy(food);
                cooldown.ActiveButton();
            }));
            
        }));
    }
}
