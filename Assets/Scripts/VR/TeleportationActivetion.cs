using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivetion : MonoBehaviour
{
    public XRRayInteractor teleportInteractors;
    public InputActionProperty teleportActivatorAction;

    public Transform playerHead; 
    public LayerMask wallLayer;   

    void Start()
    {
        teleportInteractors.gameObject.SetActive(false);
        teleportActivatorAction.action.performed += Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        teleportInteractors.gameObject.SetActive(true);
    }

    void Update()
    {
        if (teleportActivatorAction.action.WasReleasedThisFrame())
        {
            teleportInteractors.gameObject.SetActive(false);
        }
        if (teleportInteractors.gameObject.activeSelf && playerHead != null)
        {
            if (teleportInteractors.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                Vector3 targetDestination = hit.point;

                if (Physics.Linecast(playerHead.position, targetDestination, wallLayer))
                {
                    teleportInteractors.gameObject.SetActive(false);
                }
            }
        }
    }
}