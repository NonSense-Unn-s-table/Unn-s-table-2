using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PedestalChecker : MonoBehaviour
{
    public XRSocketInteractor hatInteractor;
    public GameObject hatObject;
    public XRSocketInteractor potionInteractor;
    public GameObject potionObject;
    public XRSocketInteractor staffInteractor;
    public GameObject staffObject;
    public XRSocketInteractor mushroomInteractor;
    public GameObject mushroomObject;
    public UnityEvent Activated;
    public UnityEvent Deactivated;
    // public bool debug;
    private bool hasOpened;
    private bool hatInPlace;
    private bool potionInPlace;
    private bool staffInPlace;
    private bool mushroomInPlace;
    // Start is called before the first frame update
    void Start()
    {
        hatInPlace = false;
        potionInPlace = false;
        staffInPlace = false;
        mushroomInPlace = false;
        hasOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hatInteractor.GetOldestInteractableSelected() != null) hatInPlace = hatInteractor.GetOldestInteractableSelected().Equals(hatObject.GetComponent<IXRSelectInteractable>()); 
        if (potionInteractor.GetOldestInteractableSelected() != null) potionInPlace = potionInteractor.GetOldestInteractableSelected().Equals(potionObject.GetComponent<IXRSelectInteractable>()); 
        if (staffInteractor.GetOldestInteractableSelected() != null) staffInPlace = staffInteractor.GetOldestInteractableSelected().Equals(staffObject.GetComponent<IXRSelectInteractable>()); 
        if (mushroomInteractor.GetOldestInteractableSelected() != null) mushroomInPlace = mushroomInteractor.GetOldestInteractableSelected().Equals(mushroomObject.GetComponent<IXRSelectInteractable>()); 
        // if (debug) 
        // {
        //     IXRSelectInteractable obj = hatInteractor.GetOldestInteractableSelected();
        //     Debug.Log(obj + " should be equal to " + hatObject.GetComponent<IXRSelectInteractable>());
        //     Debug.Log(obj.Equals(hatObject.GetComponent<IXRSelectInteractable>()));
        // }
        // Debug.Log(hatInPlace);
        // Debug.Log(potionInPlace);
        // Debug.Log(staffInPlace);
        // Debug.Log(mushroomInPlace);
        if(hatInPlace && potionInPlace && staffInPlace && mushroomInPlace && !hasOpened)
        {
            Activated.Invoke();
            hasOpened = true;
        }
        if(!(hatInPlace && potionInPlace && staffInPlace && mushroomInPlace) && hasOpened)
        {
            Deactivated.Invoke();
            hasOpened = false;
        }
        // Debug.Log(hasOpened);
    }

}
