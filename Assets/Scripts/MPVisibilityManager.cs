using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MPVisibilityManager : NetworkBehaviour
{
    [SerializeField] private Renderer[] objectsToHideFromClient;
    [SerializeField] private Renderer[] objectsToHideFromHost;
    [SerializeField] private Renderer[] objectsToShowForClient;
    [SerializeField] private Renderer[] objectsToShowForHost;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartRpc();
    }

    [Rpc(SendTo.Server)]
    void RestartRpc()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Second Scene", LoadSceneMode.Single);
    }

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