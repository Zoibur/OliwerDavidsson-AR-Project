using UnityEngine;

public class Swing : MonoBehaviour
{
    private static readonly int SwingTrigger = Animator.StringToHash("Swing");
    [SerializeField] private Animator swingAnimator;
    public Transform swingSeat;

    public void StartSwing()
    {
        swingAnimator.SetTrigger(SwingTrigger);
        swingAnimator.SetBool("Stop", false);
    }

    public void StopSwing()
    {
        swingAnimator.SetBool("Stop", true);
    }
   
}
