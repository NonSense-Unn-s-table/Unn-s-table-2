using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MPClientOwnershipGiver : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            ulong clientID = NetworkManager.Singleton.LocalClientId;
            ChangeOwnershipRpc(clientID);
        }
        base.OnNetworkSpawn();
    }

    [Rpc(SendTo.Server)]
    private void ChangeOwnershipRpc(ulong clientID)
    {
        NetworkObject.ChangeOwnership(clientID);
    }
}
