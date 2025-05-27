using System;
using AugmentedRealityCourse;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARSomething : MonoBehaviour
{
    
    private CustomInputActions _customInputActions;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        
        _customInputActions = new CustomInputActions();
        _customInputActions.Enable();
        
    } 
    private void OnEnable()
    {
        _customInputActions.TouchscreenGestures.TapStartPosition.started += TapStartPostion_statred; // Här lyssnar vi på händelsen touch screen
        _customInputActions.TouchscreenGestures.TapStartPosition.Enable();
    }

    private void OnDisable()
    {
        _customInputActions.TouchscreenGestures.TapStartPosition.Disable();
        _customInputActions.TouchscreenGestures.TapStartPosition.started -= TapStartPostion_statred;
    }

    private void TapStartPostion_statred(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        

            DebugManager.Instance.AddDebugMessage("Try Touch Screen! to rotate");

            Vector3 pos = context.ReadValue<Vector3>();

            Ray ray = _camera.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out RaycastHit hit, 10))
            {
                
                DebugManager.Instance.AddDebugMessage("Hit");
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    DebugManager.Instance.AddDebugMessage("Interactable");
                    interactable.Interact();
                }
            }

        
        
    }
}
