using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivetion : MonoBehaviour
{
    public XRRayInteractor teleportInteractors;
    public InputActionProperty teleportActivatorAction;
    // Start is called before the first frame update
    void Start()
    {
        teleportInteractors.gameObject.SetActive(false);
       // teleportActivatorAction.action.performed += 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
