using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

public void Open()
{
    Debug.Log("Dörren öppnas!");
    animator.SetTrigger("OpenDoor");
}

public void Close()
{
    Debug.Log("Dörren stängs!");
    animator.SetTrigger("CloseDoor");
}
}