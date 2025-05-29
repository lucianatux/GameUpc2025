using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour   // This script handles the player's key inventory.
{
    private List<Key> _keys = new List<Key>();  // A private list to store the keys the player has collected.

    public void AddKey(Key keyData)   // Adds a key to the inventory
    {
        if (!_keys.Contains(keyData))  // Check if the key is already in the inventory.
        {
            _keys.Add(keyData);    // Add the key to the list.
            Debug.Log("Llave agregada: " + keyData.displayName);  // Log the key pickup (for debugging or feedback).
        } 
    }

    public bool HasKey(string id)  // Checks if the player has a key with the specified ID.
    {
        return _keys.Any(key => key.id == id);  // Return true if any key in the list matches the given ID.
    }
}