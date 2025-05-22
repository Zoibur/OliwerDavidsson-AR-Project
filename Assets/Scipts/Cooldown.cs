using UnityEngine;
using UnityEngine.UI;
public class Cooldown : MonoBehaviour
{
 
    [SerializeField] private Button danceButton;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button eatingButton;
    [SerializeField] private Button swingButton;
    
    
    public void InactiveButton()
    {
      danceButton.interactable = false;
      actionButton.interactable = false;
      eatingButton.interactable = false;
      swingButton.interactable = false;
    }
    
    public void ActiveButton()
    {
      danceButton.interactable = true;
      actionButton.interactable = true;
      eatingButton.interactable = true;
      swingButton.interactable = true;
    }
    
}
