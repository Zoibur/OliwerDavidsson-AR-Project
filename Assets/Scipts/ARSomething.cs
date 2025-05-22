using System;
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
        
    } 
    private void OnEnable()
    {
        _customInputActions.ARMobil.Touch.started += TapStartPostion_statred; // Här lyssnar vi på händelsen touch screen
        _customInputActions.ARMobil.Touch.Enable();
    }

    private void OnDisable()
    {
        _customInputActions.ARMobil.Touch.Disable();
        _customInputActions.ARMobil.Touch.started -= TapStartPostion_statred;
    }

    private void TapStartPostion_statred(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector3 pos = context.ReadValue<Vector3>();
        
        Ray ray = _camera.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit,10))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable !=null)
            {
                interactable.Interact();
            }
        }
    }
}
