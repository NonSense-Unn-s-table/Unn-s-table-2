using UnityEngine;
using UnityEngine.Events;

public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    private Collider MPCube;
    public UnityEvent Activated;
    public UnityEvent Deactivated;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.Equals(MPCube)){
            Debug.Log("MPCube är på!");
            Activated.Invoke();
        }
    }
    void OnTriggerExit(Collider other){
        if (other.Equals(MPCube))
        {
            Debug.Log("MPCube lämnade!");
            Deactivated.Invoke();
        }
    }
}
