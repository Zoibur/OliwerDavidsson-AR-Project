using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using AugmentedRealityCourse;


public class ARTapToPlace : MonoBehaviour
{
   [SerializeField] private GameObject refToPrefab; 
   
   [SerializeField] private DisablePlaneTracking planeHandle; 
   
    private ARRaycastManager raycastManager;    

    private ARPlaneManager planeManager;        
    
    private bool isPlaced = false;

    private CustomInputActions customInputAction;   
    
    private List<ARRaycastHit> raycasthits = new();
   

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();  

        if (raycastManager == null )
        {
            DebugManager.Instance.AddDebugMessage("You missing a reference to raycastmanager component, please add it to the RIG");
            return;
        }

        
        customInputAction = new CustomInputActions();   
        customInputAction.Enable();
        
        
        
    }
  

    private void OnEnable()
    {
        
              customInputAction.ARMobil.Touch.started += Touch_started; // Här lyssnar vi på händelsen touch screen
              customInputAction.ARMobil.Touch.Enable();  // Här aktiverar avlyssningen av den händelsen  
              
    }

    private void OnDisable()
    {
        
        customInputAction.ARMobil.Touch.Disable();
        customInputAction.ARMobil.Touch.started -= Touch_started;
    }
    /// <summary>
    /// Om händelsen har utlösts så fångar denna funktion nu upp denna händelse
    /// </summary>
    /// <param name="obj"></param>
    private void Touch_started(InputAction.CallbackContext context)
    {
        if (isPlaced == false)
        {
          try
            {
                // Här gör vi någonting som sker när vi tryckte på skärmen...
    
                // Exempelvis skriv ut meddelande
                
    
                //DebugManager.Instance.AddDebugMessage("Postion " + context.ReadValue<Vector2>());
    
                Vector2 screenPoint = context.ReadValue<Vector2>();
    
                if (raycastManager.Raycast(screenPoint, raycasthits, TrackableType.Planes))
                {
                    
    
                    Pose hitpose = raycasthits[0].pose;
    
                    GameObject refToGameObject = Instantiate(refToPrefab, hitpose.position, hitpose.rotation);
    
                    refToGameObject.name = "Ref To Object";
                    refToGameObject.SetActive(true);
                    refToGameObject.AddComponent<ARAnchor>();
                    
                    planeHandle.SetPlanesVisibility(false);
                    planeHandle.SetPlaneTracking(false);
                    
                    isPlaced = true;

                }
                
                
            }
            catch (Exception err)
            {
                DebugManager.Instance.AddDebugMessage("Failed to Touch Screen! " + err.Message);
            }    
        }
           
        
       
       
       
    }
}
