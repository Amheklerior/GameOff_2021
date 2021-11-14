using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Voiceline
{
    [TextArea(3,10)]
    public string text;
    [Tooltip("Time (in seconds) to wait before next voiceline")]
    public float pause;
}
