using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using AugmentedRealityCourse;

public class DisablePlaneTracking : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;

    private void Awake()
    {
        arPlaneManager =  GetComponentInParent<ARPlaneManager>();

        if (arPlaneManager == null)
        {
            DebugManager.Instance.AddDebugMessage("Missing Plane Manager");
            return;
        }
    }
    
    public void SetPlaneTracking(bool isActivated)
    {
        arPlaneManager.enabled = isActivated;
    }
    
    public void SetPlanesVisibility(bool isVisible)
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(isVisible);
        }
    }
    
    public void ResetPlaneTracking() {

        arPlaneManager.enabled = false; 

        foreach (var plane in arPlaneManager.trackables)
        {
            Destroy(plane.gameObject);
        }

        arPlaneManager.enabled = true;
    }
}


