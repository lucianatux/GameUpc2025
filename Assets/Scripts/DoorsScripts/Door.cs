using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private List<Key> requiredKeys;

    [Header("Components")]
    [SerializeField] private Collider2D blockingCollider;
    [SerializeField] private Animator animator;

    [Header("Room assignment")]
    public int roomID;

    [Header("Visual Effects")]
    [SerializeField] private GameObject openEffect;

    [Header("Comportamiento especial")]
    [SerializeField] private bool neverAutoOpen = false;
    public bool NeverAutoOpen => neverAutoOpen;

    private bool _isOpen = false;
    private bool _isLocked = false;
    private bool _openedByDefault = false;

    private void Awake()
    {
        if (blockingCollider == null)
            blockingCollider = GetComponent<Collider2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!RequiresKeys() && !neverAutoOpen)
        {
            _openedByDefault = true;
            Open();
        }
        else
        {
            Close(); // Opcional si querés asegurarte
        }
    }

    public void SetLocked(bool locked)
    {
        _isLocked = locked;
    }

    public void Open()
    {
        if (_isOpen) return;

        _isOpen = true;
        animator?.SetTrigger("Open");
        EnvironmentEventsManager.Instance?.DoorOpen(); 
        if (blockingCollider != null)
            blockingCollider.enabled = false;
        
          if (neverAutoOpen && openEffect != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 0, 0); 
            Instantiate(openEffect, spawnPos, Quaternion.identity);
            EnvironmentEventsManager.Instance?.LevelComplete(); 
        }
    }

    public void Close()
    {
        if (!_isOpen) return;

        _isOpen = false;
        animator?.SetTrigger("Close");
        EnvironmentEventsManager.Instance?.DoorClose(); 
        if (blockingCollider != null)
            blockingCollider.enabled = true;
    }

    public void TryOpen(PlayerInventory inventory)
    {
        if (_isOpen)
        {
            Debug.Log("Door is already open.");
            return;
        }

        if (_isLocked)
        {
            Debug.Log("Door is locked by RoomManager until waves are completed.");
            return;
        }

        if (!RequiresKeys())
        {
            Debug.Log("No keys required, opening directly.");
            Open();
            return;
        }

        if (requiredKeys.Any(key => !inventory.HasKey(key.id)))
        {
            Debug.Log("You're missing keys to open this door!");
            return;
        }

        Debug.Log("All keys present. Unlocking door.");
        Open();
    }

    public void TryAutoOpen()
    {
        if (_isOpen) return;

        if (!RequiresKeys() && !neverAutoOpen)
        {
            Debug.Log($"[AutoOpen] Door in Room {roomID} auto-opening.");
            Open();
        }
        else
        {
            Debug.Log($"[AutoOpen] Door in Room {roomID} did not auto-open. RequiresKeys: {RequiresKeys()}, NeverAutoOpen: {neverAutoOpen}");
        }
    }

    public bool RequiresKeys() => requiredKeys != null && requiredKeys.Count > 0;
}
