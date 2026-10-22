using Unity.Netcode;
using UnityEngine;

public class MultiplayerCube : NetworkBehaviour
{
    [SerializeField]
    private MeshRenderer rend;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Camera tableCamera;
    [SerializeField]
    private BoxCollider bcoll;
    [SerializeField]
    private int byteTag;
    void Awake()
    {
        if (!rend) rend = GetComponent<MeshRenderer>();
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!tableCamera) tableCamera = FindAnyObjectByType<Camera>();
        if (!bcoll) bcoll = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ScreenObject cube;
        Vector2 cubeScreenPosition = Vector2.zero;

        if (TUIOSupport.emulateTuio)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                
                cube = new ScreenObject
                {
                    id = 0,
                    classId = byteTag,
                    screenPosition = mousePosition,
                    angle = 0
                };

                cubeScreenPosition = cube.screenPosition;
            } 
            else
            {
                cube = null;
            }
            
        }
        else
        {
            // Find any "cube" on PixelSense
            cube = TUIOSupport.GetFirstScreenObject(byteTag);
            // Debug.Log(cube);
            if (cube != null) {
                cubeScreenPosition = cube.screenPosition * new Vector2(Screen.width, Screen.height);
            }
        }


        if (cube != null)
        {
            UnhideCubeRpc();

            RaycastHit hitInfo;
            Ray ray = tableCamera.ScreenPointToRay(cubeScreenPosition);
            Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Floor", "Objects"));
            Vector3 cubePosition = hitInfo.point;

            // Debug.Log("Cube position: " + cubePosition);
            // if (Vector2.Distance(cubePosition, Vector2.zero) > 0.1f) 
            MoveCubeRpc(cubePosition + new Vector3(0f,1f,0f));
        } 
        else
        {
            // HideCubeRpc();
        }
    }

    [Rpc(SendTo.Everyone)]
    public void MoveCubeRpc(Vector3 position)
    {
        rb.MovePosition(position);
        rb.velocity = Vector3.zero;
    }

    [Rpc(SendTo.Everyone)]
    public void HideCubeRpc()
    {
        rend.enabled = false;
        bcoll.enabled = false;
    }

    [Rpc(SendTo.Everyone)]
    public void UnhideCubeRpc()
    {
        rend.enabled = true;
        bcoll.enabled = true;
    }
}
