
using UnityEngine;

[CreateAssetMenu(fileName = "Hat")]
public class Hat : ScriptableObject
{
    public string ItemName;
    public int itemPrice;
    public Sprite Thumbnail;
    public GameObject Model;
}
