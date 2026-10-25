using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SceneChanger : NetworkBehaviour
{
    public string targetScene = "";

    [SerializeField]
    private bool changeSceneOnConnect = true;

    private void Awake()
    {
        if (targetScene == "")
        {
            Debug.LogWarning("No target scene set in SceneChanger!");
        }
    }

    public override void OnNetworkSpawn()
    {
        if (changeSceneOnConnect)
        {
            ChangeScene();
        }
        
        base.OnNetworkSpawn();
    }

    public void ChangeScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene(targetScene, UnityEngine.SceneManagement.LoadSceneMode.Single);
        NetworkObject.Despawn();
    }
}
