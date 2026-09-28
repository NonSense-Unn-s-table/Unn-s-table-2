using UnityEngine;

public class LayerChanger : MonoBehaviour
{

    public string targetLayerName = "Floor";
    public bool applyToChildren = true;
    public void ChangeLayer()
    {
        int newLayer = LayerMask.NameToLayer(targetLayerName);

        if (newLayer == -1)
        {
            Debug.LogError("Layer '" + targetLayerName + "' does not exist! Make sure it is defined in Unity's Tags and Layers.");
            return;
        }

        if (applyToChildren)
        {
            SetLayerRecursive(gameObject, newLayer);
        }
        else
        {
            gameObject.layer = newLayer;
        }

        Debug.Log("Successfully changed layer to: " + targetLayerName);
    }

    private void SetLayerRecursive(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, newLayer);
        }
    }
}