using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    [SerializeField]
    private Collider Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) RestartRpc();
    }

    [Rpc(SendTo.Owner)]
    void RestartRpc()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Second Scene", LoadSceneMode.Single);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player))
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Second Scene", LoadSceneMode.Single);
        }
    }
}
