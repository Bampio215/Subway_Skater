using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Start is called before the first frame update
    public float chunklength;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Chunk ShowChunk()
    {
        gameObject.SetActive(true);
        return this;
    }
    public Chunk HideChunk()
    {
        gameObject.SetActive(false);
        return this;
    }
}
