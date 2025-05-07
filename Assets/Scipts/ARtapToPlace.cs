using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using AugmentedRealityCourse;

public class ARtapToPlace : MonoBehaviour
{
   [SerializeField]
    private GameObject refToPrefab;             // Detta är referens till den prefab som ska ska renderas ut på skärmen

    private ARRaycastManager raycastManager;    // För att ha access till raycastmanager komponenten

    private ARPlaneManager planeManager;        // För att ha access till planemanager komponenten

    private CustomInputActions customInputAction;    // Referens till vår customized Input Action

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();  // skaffa referens till komponenten ARRaycastManager

        if (raycastManager == null )
        {
            DebugManager.Instance.AddDebugMessage("You missing a reference to raycastmanager component, please add it to the RIG");
            return;
        }

        // Nu blir nästa steg att skapa en "objekt" av vår Customized Input Action fil
        //customInputAction = new CustomInputAction();

        customInputAction.Enable();

        // customInputAction.ARMobile.Touch
        customInputAction.ARMobil.Touch.started += Touch_started; // Här lyssnar vi på händelsen touch screen
        customInputAction.ARMobil.Touch.Enable();  // Här aktiverar avlyssningen av den händelsen
    }

    /// <summary>
    /// Om händelsen har utlösts så fångar denna funktion nu upp denna händelse
    /// </summary>
    /// <param name="obj"></param>
    private void Touch_started(InputAction.CallbackContext obj)
    {
        // Här gör vi någonting som sker när vi tryckte på skärmen...

        // Exempelvis skriv ut meddelande
        DebugManager.Instance.AddDebugMessage("Touch Screen!");
    }
}
