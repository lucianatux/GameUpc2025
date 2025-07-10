using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Thought", menuName = "Narrative/Thought")]
public class ThoughtSO : ScriptableObject
{
    [TextArea]
    public string text;
    public float duration = 3f;
    public bool destroyAfterShown = true; 
}
