
using UnityEngine;

public class ObjectUnhider : MonoBehaviour
{
    public MeshRenderer rend;
    public BoxCollider bcoll;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reveal()
    {
        rend.enabled = true;
        bcoll.enabled = true;
        rb.useGravity = true;
    }

    public void Hide()
    {
        rend.enabled = false;
        bcoll.enabled = false;
        rb.useGravity = false;
    }
}
