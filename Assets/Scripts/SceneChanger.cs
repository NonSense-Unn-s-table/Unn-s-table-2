using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SceneChanger : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("MP UI Bugfix and resize", UnityEngine.SceneManagement.LoadSceneMode.Single);
        NetworkObject.Despawn();
        base.OnNetworkSpawn();
    }
}
