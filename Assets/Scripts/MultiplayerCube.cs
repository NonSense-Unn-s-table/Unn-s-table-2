using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]

public class MultiplayerCube : NetworkBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private Camera tableCamera;
    
    [SerializeField]
    private int byteTag;
    [SerializeField]
    [Min(0)]
    private float height = 1;

    private enum MouseButton
    {
        LeftMouseButton = 0,
        RightMouseButton = 1,
        MiddleMouseButton = 2
    }

    [SerializeField]
    private MouseButton emulatorMouseButton;

    private Vector3 lastUsedCubePosition = Vector2.zero;

    public UnityEvent hideCube;
    public UnityEvent unhideCube;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!tableCamera)
        {
            Debug.LogWarning("Overhead table camera not assigned. Using first camera found.");
            tableCamera = FindAnyObjectByType<Camera>();
        }
    }

    void Update()
    {
        ScreenObject cube;
        Vector2 cubeScreenPosition = Vector2.zero;

        if (TUIOSupport.emulateTuio)
        {
            if (Input.GetMouseButton((int)emulatorMouseButton))
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
            if (Vector3.Distance(cubePosition, lastUsedCubePosition) > 0.1f)
            {
                Debug.Log("djwaud: " + Vector3.Distance(cubePosition, lastUsedCubePosition));
                lastUsedCubePosition = cubePosition;
                MoveCubeRpc(cubePosition + new Vector3(0f, height, 0f));
            }
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
        hideCube.Invoke();
    }

    [Rpc(SendTo.Everyone)]
    public void UnhideCubeRpc()
    {
        unhideCube.Invoke();
    }
}
