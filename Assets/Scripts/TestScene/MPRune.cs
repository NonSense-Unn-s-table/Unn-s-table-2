using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

public class MPRune : NetworkBehaviour
{
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;

        PressRpc();
    }

    [Rpc(SendTo.Everyone)]
    void PressRpc()
    {
        onPressed.Invoke();
    }
}
