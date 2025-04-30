using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Key> _keys = new List<Key>();

    public void AddKey(Key keyData)
    {
        if (!_keys.Contains(keyData))
        {
            _keys.Add(keyData);
            Debug.Log("Llave agregada: " + keyData.displayName);
        }
    }

    public bool HasKey(string id)
    {
        return _keys.Any(key => key.id == id);
    }
}