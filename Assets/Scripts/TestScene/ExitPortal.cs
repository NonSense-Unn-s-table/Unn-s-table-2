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

    void OnTriggerEnter(Collider other)
    {
        if (other.Equals(Player))
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Second Scene", LoadSceneMode.Single);
        }
    }
}
