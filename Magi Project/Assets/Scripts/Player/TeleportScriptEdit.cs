using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportScriptEdit : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;
    private InputAction thumbstick;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        thumbstick.Enable();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (!isActive)
            return;

        if (thumbstick.triggered)
            return;

        if(!rayInteractor.GetCurrentRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled=false;
            isActive = false;
        }

        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point
        };

        provider.QueueTeleportRequest(request);
        rayInteractor.enabled = false;
        isActive = false;

    }

    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
        isActive = true;
    }

    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = false;
        isActive = false;
    }
}
