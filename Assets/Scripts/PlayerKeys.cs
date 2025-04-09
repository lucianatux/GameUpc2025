using UnityEngine;

public class PlayerKeys : MonoBehaviour
{
    public bool hasRedKey = false;
    public bool hasBlueKey = false;
    public bool hasBossKey = false;

    public void PickUpKey(string keyID)
    {
        switch (keyID)
        {
            case "red":
                hasRedKey = true;
                break;
            case "blue":
                hasBlueKey = true;
                break;
            case "boss":
                hasBossKey = true;
                break;
        }

        Debug.Log("Llave obtenida: " + keyID);
    }

    public bool HasKey(string keyID)
    {
        return (keyID == "red" && hasRedKey) ||
               (keyID == "blue" && hasBlueKey) ||
               (keyID == "boss" && hasBossKey);
    }
}
