using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ScriptToggle : MonoBehaviour
{
          
    [SerializeField]private Button markerlessButton;    
    [SerializeField]private Button imageTrackingButton; 
    public ARPlaneManager planeManager;  
    public ARTrackedImageManager imageManager;
    void Start()
    {
        // Disable both AR modes at start
        if (planeManager != null)
        {
            planeManager.enabled = false;
            planeManager.requestedDetectionMode = PlaneDetectionMode.None;
        }

        if (imageManager != null)
        {
            imageManager.enabled = false;
        }

        // Set up button listeners
        markerlessButton.onClick.AddListener(() =>
        {
            EnableMarkerlessAR();
            HideButtons();
        });

        imageTrackingButton.onClick.AddListener(() =>
        {
            EnableImageTrackingAR();
            HideButtons();
        });
    }

    void EnableMarkerlessAR()
    {
        if (planeManager != null && imageManager != null)
        {
            planeManager.enabled = true;
            planeManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;

            imageManager.enabled = false;
            Debug.Log("Markerless AR enabled, Image Tracking AR disabled");
        }
    }
  

    void EnableImageTrackingAR()
    {
        if (planeManager != null && imageManager != null)
        {
            planeManager.enabled = false;

            imageManager.enabled = true;
            Debug.Log("Image Tracking AR enabled, Markerless AR disabled");
        }
    }

     void HideButtons()
    {
        markerlessButton.gameObject.SetActive(false);
        imageTrackingButton.gameObject.SetActive(false);
    }

}
