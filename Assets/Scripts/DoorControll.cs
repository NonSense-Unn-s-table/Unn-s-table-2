using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

public void Open()
{
    animator.SetTrigger("OpenDoor");
}

public void Close()
{
    animator.SetTrigger("CloseDoor");
}
}