using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    public Scene sc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        NetworkManager.Singleton.SceneManager.LoadScene("test mp ui", LoadSceneMode.Single);
    }
}
