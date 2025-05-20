using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Items/Key")]
public class Key : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
}