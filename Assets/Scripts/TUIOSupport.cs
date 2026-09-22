using System;
using System.Collections.Generic;
using System.Linq;
using TUIOsharp;
using TUIOsharp.DataProcessors;
using UnityEngine;
public class ScreenObject
{
    public int id;
    public int classId;
    public Vector2 screenPosition;
    public float angle;

}

public class CursorObject
{
    public int id;
    public Vector2 screenPosition;
}

public class TUIOSupport : MonoBehaviour
{
    private static TUIOSupport _instance;
    private readonly int _port = 3333;
    private TuioServer tuioServer;
    private Dictionary<int, ScreenObject> _screenObjects;
    private Dictionary<int, CursorObject> _screenCursors;

    [SerializeField]
    private bool EmulateTuio = false;
    public static bool emulateTuio;
    [SerializeField]
    private int PositionFactorX = 80; // was 160
    public static int positionFactorX;
    [SerializeField]
    private int PositionFactorY = 45; // was 90
    public static int positionFactorY;

    public void ApplySerialize() {
        emulateTuio = EmulateTuio;
        positionFactorX = PositionFactorX;
        positionFactorY = PositionFactorY;
    }


    // Start is called before the first frame update
    private void Start()
    {
        ApplySerialize();
        if (emulateTuio) return;
        Debug.Log(" HUHHHH");
        _screenObjects = new Dictionary<int, ScreenObject>();
        ListenForTUIO();
        _instance = this;
        Debug.Log("Started TUIO Client");
    }
    private void OnDestroy()
    {
        if (emulateTuio) return;
        tuioServer.Disconnect();
        tuioServer = null;
        _instance = null;
        _screenObjects = null;
    }
    private void ListenForTUIO()
    {
        // tuio
        tuioServer = new TuioServer(_port);
        tuioServer.Connect();

        Debug.Log(string.Format("TUIO listening on port {0}.", _port));

        // Add Object Processor
        var objectProcessor = new ObjectProcessor();
        objectProcessor.ObjectAdded += (sender, e) =>
        {
            if (_screenObjects.ContainsKey(e.Object.Id))
            {
                Debug.LogError("A screen object input was received for an object that already exists.");
                return;
            }

            var obj = new ScreenObject()
            {
                id = e.Object.Id,
                classId = e.Object.ClassId,
                screenPosition = new Vector2(e.Object.X, 1 - e.Object.Y),
                angle = e.Object.Angle
            };

            _screenObjects.Add(e.Object.Id, obj);

            Debug.Log("Added object with ByteTag " + e.Object.ClassId + " at position " + obj.screenPosition);
        };

        objectProcessor.ObjectUpdated += (sender, e) =>
        {
            if (!_screenObjects.ContainsKey(e.Object.Id))
            {
                Debug.LogError("Tried to update a screen object that was not added priorly.");
                return;
            }

            var go = _screenObjects[e.Object.Id];
            go.screenPosition = new Vector2(e.Object.X, 1 - e.Object.Y);
            go.angle = e.Object.Angle;

            Debug.Log("Updated object with ByteTag " + e.Object.ClassId + " at pos: " + go.screenPosition);
        };
        ;
        objectProcessor.ObjectRemoved += (sender, e) =>
        {
            if (!_screenObjects.ContainsKey(e.Object.Id))
            {
                Debug.LogError("Tried to remove a screen object that was not added priorly.");
                return;
            }

            _screenObjects.Remove(e.Object.Id);
            
            Debug.Log("Removed object with ByteTag " + e.Object.ClassId);
        };
        tuioServer.AddDataProcessor(objectProcessor);


        // Add Cursor Processor
        var cursorProcessor = new CursorProcessor();
        cursorProcessor.CursorAdded += (sender, e) =>
        {
            if (_screenCursors.ContainsKey(e.Cursor.Id))
            {
                Debug.LogError("A screen cursor input was received for a cursor that already exists.");
                return;
            }

            var cur = new CursorObject()
            {
                id = e.Cursor.Id,
                screenPosition = new Vector2(e.Cursor.X, 1 - e.Cursor.Y)
            };

            // _screenCursors.Add(e.Cursor.Id, cur);

            Debug.Log("Added cursor with ID " + e.Cursor.Id + " at position " + cur.screenPosition);
        };
        tuioServer.AddDataProcessor(cursorProcessor);
    }

    public static IEnumerable<ScreenObject> GetScreenObjects()
    {
        Debug.Log(_instance._screenObjects.Values);
        return _instance._screenObjects.Values;
    }

    public static IEnumerable<ScreenObject> GetScreenObjects(int classId)
    {
        return _instance._screenObjects.Values.Where(obj => obj.classId == classId);
    }

    public static ScreenObject GetFirstScreenObject(int classId)
    {
        try
        {
            return _instance._screenObjects.Values.First(obj => obj.classId == classId);
        } 
        catch (Exception e)
        {
            return null;
        }
    }
}