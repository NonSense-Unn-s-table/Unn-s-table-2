using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : NetworkBehaviour
{
    void Start()
    {
        if (IsHost && !IsLocalPlayer)
        {
            Destroy(gameObject);
        }
    }
}
