using UnityEngine;


public class Forcefield : MonoBehaviour
{
    // public PhysicsButton button;
    private MeshRenderer rend;
    private BoxCollider bcoll;
    [SerializeField] private LayerChanger layerChanger;
    // Start is called before the first frame update
    void Start()
    {
        if (!rend) rend = GetComponent<MeshRenderer>();
        if (!bcoll) bcoll = GetComponent<BoxCollider>();
        
        // button.onPressed.AddListener(RemoveForcefield);
        // button.onReleased.AddListener(DeployForcefield);
    }
    
    public void RemoveForcefield()
    {
        rend.enabled = false;
        bcoll.enabled = false;
        layerChanger.ChangeLayer();
    }

    public void DeployForcefield()
    {
        rend.enabled = true;
        bcoll.enabled = true;
        layerChanger.ChangeLayerBack();
    }
}
