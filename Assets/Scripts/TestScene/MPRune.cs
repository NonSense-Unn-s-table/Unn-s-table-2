using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

public class MPRune : NetworkBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    [SerializeField]
    public MeshRenderer inactiveRuneMesh;
    [SerializeField]
    public MeshRenderer activeRuneMesh;
    [SerializeField]
    private Collider Player;
    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;

        if (other.Equals(Player))
        {
            PressRpc();            
        }
    }

    [Rpc(SendTo.Everyone)]
    void PressRpc()
    {
        inactiveRuneMesh.enabled = false;
        activeRuneMesh.enabled = true;
        onPressed.Invoke();
    }
}
