using UnityEngine;

public class PlayerPixelsenseInput : MonoBehaviour
{
    private TUIOSupport tuio;
    private MeshRenderer rend;
    private Rigidbody rb;
    [SerializeField]
    private Camera tableCamera;

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
            if (Input.GetMouseButton(1))
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
            cube = TUIOSupport.GetFirstScreenObject(1);
            if (cube != null) cube.screenPosition *= new Vector2(Screen.width, Screen.height);
        }


        if (cube != null)
        {
            UnhideCube();

            RaycastHit hitInfo;
            Ray ray = tableCamera.ScreenPointToRay(cube.screenPosition);
            Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Floor"));
            Vector3 cubePosition = hitInfo.point;

            Debug.Log("Player position: " + cubePosition);
            if (Vector2.Distance(cubePosition, Vector2.zero) > 0.1f) rb.MovePosition(cubePosition + new Vector3(0f,1f,0f));
        } 
        else
        {
            // HideCube();
        }
    }

    public void HideCube()
    {
        rend.enabled = false;
    }

    public void UnhideCube()
    {
        rend.enabled = true;
    }
}
