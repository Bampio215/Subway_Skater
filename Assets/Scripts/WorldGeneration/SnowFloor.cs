
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    public float offsetSpeed = 0.85f;
    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
        material.SetVector("_offset", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}
