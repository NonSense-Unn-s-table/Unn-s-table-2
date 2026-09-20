using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    override public void OnNetworkSpawn()
    {
        if (IsHost && !IsLocalPlayer)
        {
            NetworkObject.Despawn();
        }
        else // for host/VR player
        {
            Camera playerCamera = GetComponentInChildren<Camera>();
            playerCamera.enabled = true;
        }
    }
}
