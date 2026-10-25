using UnityEngine;
using Unity.Netcode;

public class MPVisibilityManager : NetworkBehaviour
{
    [SerializeField] private Renderer[] objectsToHideFromClient;
    [SerializeField] private Renderer[] objectsToHideFromHost;
    [SerializeField] private Renderer[] objectsToShowForClient;
    [SerializeField] private Renderer[] objectsToShowForHost;

    public override void OnNetworkSpawn()
    {
        Renderer[] objectsToHide = (IsHost) ? objectsToHideFromHost : objectsToHideFromClient;
        Renderer[] objectsToShow = (IsHost) ? objectsToShowForHost : objectsToShowForClient;

        foreach (Renderer rend in objectsToHide)
        {
            rend.enabled = false;
        }

        foreach (Renderer rend in objectsToShow)
        {
            rend.enabled = true;
        }

        base.OnNetworkSpawn();
    }
}