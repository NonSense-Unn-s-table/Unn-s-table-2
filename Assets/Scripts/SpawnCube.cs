using Unity.Netcode;
using UnityEngine;

public class SpawnCube : NetworkBehaviour
{
    private TUIOSupport tuio;
    private MeshRenderer rend;
    private Rigidbody rb;
    [SerializeField] private Camera tableCamera;

    void Awake()
    {
        tuio = FindAnyObjectByType<TUIOSupport>();
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        if (!tableCamera) tableCamera = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        ScreenObject cube;
        if (TUIOSupport.emulateTuio)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                
                cube = new ScreenObject
                {
                    id = 0,
                    classId = 0,
                    screenPosition = mousePosition,
                    angle = 0
                };
            } 
            else
            {
                cube = null;
            }
            
        }
        else
        {
            // Find any "cube" on PixelSense
            cube = TUIOSupport.GetFirstScreenObject(0);
            Debug.Log(cube);
            if (cube != null) cube.screenPosition *= new Vector2(Screen.width, Screen.height);
        }


        if (cube != null)
        {
            UnhideCubeRpc();

            RaycastHit hitInfo;
            Ray ray = tableCamera.ScreenPointToRay(cube.screenPosition);
            Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Floor", "Objects"));
            Vector3 cubePosition = hitInfo.point;

            Debug.Log("Cube position: " + cubePosition);
            if (Vector2.Distance(cubePosition, Vector2.zero) > 0.1f) MoveRpc(cubePosition + new Vector3(0f,1f,0f));
        } 
        else
        {
            // HideCubeRpc();
        }
    }

    [Rpc(SendTo.Owner)]
    private void MoveRpc(Vector3 position)
    {
        rb.MovePosition(position);
    }

    [Rpc(SendTo.Owner)]
    public void HideCubeRpc()
    {
        rend.enabled = false;
    }

    [Rpc(SendTo.Owner)]
    public void UnhideCubeRpc()
    {
        rend.enabled = true;
    }
}
