using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se encarga únicamente de recoger el input del jugador.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
}
