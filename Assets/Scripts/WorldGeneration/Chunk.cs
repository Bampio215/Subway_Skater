
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Start is called before the first frame update
    public float chunklength;

    public Chunk ShowChunk()
    {
        transform.gameObject.BroadcastMessage("OnShowChunk", SendMessageOptions.DontRequireReceiver);
        gameObject.SetActive(true);
        return this;
    }
    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
