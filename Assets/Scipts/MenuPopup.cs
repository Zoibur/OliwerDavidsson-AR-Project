using UnityEngine;
using DG.Tweening;

public class MenuPopup : MonoBehaviour
{
    public RectTransform menuPanel;
    public float duration = 0.3f;
    private bool isOpen = false;
 
    void Start()
    {
        menuPanel.localScale = Vector3.zero;
    }
 
    public void ToggleMenu()
    {
        if (isOpen)
        {
            menuPanel.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);
        }
        else
        {
            menuPanel.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        }
 
        isOpen = !isOpen;
    }
}
