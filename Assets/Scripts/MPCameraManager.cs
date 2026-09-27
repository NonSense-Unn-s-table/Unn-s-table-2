using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MPCameraManager : NetworkBehaviour
{
    [SerializeField] Camera VRCamera;
    [SerializeField] Camera tableCamera;

    public override void OnNetworkSpawn()
    {
        if (!VRCamera || !tableCamera)
        {
            Debug.Log("Please assign cameras to the player spawner");
            return;
        }

        if (IsHost)
        {
            VRCamera.enabled = true;
            tableCamera.enabled = false;
        }
        base.OnNetworkSpawn();
    }
}