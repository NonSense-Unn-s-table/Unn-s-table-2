using UnityEngine;


public class Forcefield : MonoBehaviour
{
    // public PhysicsButton button;
    private MeshRenderer rend;
    private BoxCollider bcoll;

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
    }

    public void DeployForcefield()
    {
        rend.enabled = true;
        bcoll.enabled = true;
    }
}
