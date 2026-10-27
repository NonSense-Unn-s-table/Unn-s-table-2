
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public MeshRenderer openmesh;
    public MeshRenderer closedmesh;
    public bool alwaysOpen;
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        if (!alwaysOpen)
        {
            closedmesh.enabled = true;
            openmesh.enabled = false;
        } else
        {
            openmesh.enabled = true;
        }
    }

    public void Open()
    {
        animator.SetTrigger("OpenDoor");
        closedmesh.enabled = false;
        openmesh.enabled = true;
    }

    public void Close()
    {
        animator.SetTrigger("CloseDoor");
        closedmesh.enabled = true;
        openmesh.enabled = false;
    }
}