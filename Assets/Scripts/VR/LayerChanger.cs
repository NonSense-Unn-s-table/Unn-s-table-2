using UnityEngine;

public class LayerChanger : MonoBehaviour
{

    public string targetLayerName = "Teleport Area";

    public string OriginalLayerName = "Wall";
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

        Debug.Log("Successfully changed layer to: " + OriginalLayerName);
    }

    public void ChangeLayerBack()
    {
        int newLayer = LayerMask.NameToLayer(OriginalLayerName);

        if (newLayer == -1)
        {
            Debug.LogError("Layer '" + OriginalLayerName + "' does not exist! Make sure it is defined in Unity's Tags and Layers.");
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

        Debug.Log("Successfully changed layer to: " + OriginalLayerName);
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