using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Items/Key")] // This attribute makes it possible to create a Key asset from the Unity Editor menu.
public class Key : ScriptableObject
{
    public string id;    // A unique identifier for the key
    public string displayName;   // The name of the key shown in the UI or debug logs.
    public Sprite icon;  // The visual icon of the key
}