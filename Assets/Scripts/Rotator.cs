using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private TUIOSupport tuio;
    [SerializeField]
    private Camera tableCamera;

    public int currentCubeID;
    private Vector2 locator2;
    private Vector2 locator3;
    

    void Start()
    {
        if (!tuio) tuio = FindAnyObjectByType<TUIOSupport>();
        if (!tableCamera) tableCamera = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        ScreenObject pos2;
        ScreenObject pos3;
        if (TUIOSupport.emulateTuio)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Vector2 mousePosition = Input.mousePosition;
                
                pos2 = new ScreenObject
                {
                    id = 2,
                    classId = 2,
                    screenPosition = mousePosition,
                    angle = 0
                };
                pos3 = null;
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Vector2 mousePosition = Input.mousePosition;
                pos3 = new ScreenObject
                {
                    id = 3,
                    classId = 3,
                    screenPosition = mousePosition,
                    angle = 0
                };
                pos2 = null;
            } 
            else
            {
                pos2 = null;
                pos3 = null;
            }
            
        }
        else
        {
            // Find any "cube" on PixelSense
            pos2 = TUIOSupport.GetFirstScreenObject(2);
            pos3 = TUIOSupport.GetFirstScreenObject(3);
            if (pos2 != null) pos2.screenPosition *= new Vector2(Screen.width, Screen.height);
            if (pos3 != null) pos3.screenPosition *= new Vector2(Screen.width, Screen.height);
        }

        // If we wanna display rotator on screen?
        // if (pos2 != null)
        // {
        //     RaycastHit hitInfo;
        //     Ray ray = tableCamera.ScreenPointToRay(pos2.screenPosition);
        //     Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Floor", "Objects"));
        //     Vector3 pointpos = hitInfo.point;

        //     Debug.Log("Cube position: " + pointpos);
        //     if (Vector2.Distance(pointpos, Vector2.zero) > 0.1f) rb.MovePosition(pointpos);
        // } 
        // if (pos3 != null)
        // {
        //     RaycastHit hitInfo;
        //     Ray ray = tableCamera.ScreenPointToRay(pos3.screenPosition);
        //     Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Floor", "Objects"));
        //     Vector3 pointpos = hitInfo.point;

        //     Debug.Log("Cube position: " + pointpos);
        //     if (Vector2.Distance(pointpos, Vector2.zero) > 0.1f) rb.MovePosition(pointpos);
        // } 
        
        if (pos2 != null) locator2 = pos2.screenPosition;
        if (pos3 != null) locator3 = pos3.screenPosition;

        if (locator2 != null & locator3 != null)
        {
            float angle = Vector2.SignedAngle(new Vector2(1, 0), locator3 - locator2);
            Debug.Log(angle);
        }
    }
}
