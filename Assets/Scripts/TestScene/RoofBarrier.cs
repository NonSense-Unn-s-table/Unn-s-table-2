using UnityEngine;

public class RoofBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    public Rune runeTrigger;
    private MeshRenderer rend;
    private MeshCollider mcoll;

    // Start is called before the first frame update
    void Start()
    {
        if (!rend) rend = GetComponent<MeshRenderer>();
        if (!mcoll) mcoll = GetComponent<MeshCollider>();
        runeTrigger.onPressed.AddListener(RemoveForcefield);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RemoveForcefield()
    {
        rend.enabled = false;
        mcoll.enabled = false;
    }
}
