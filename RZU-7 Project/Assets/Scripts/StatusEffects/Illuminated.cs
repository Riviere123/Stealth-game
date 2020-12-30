using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple illuminated status effect.
/// </summary>
[SerializeField]
public class Illuminated : StatusEffect
{
    /// <summary>
    /// instantiates the illuminated class and sets the statusName
    /// </summary>
    /// <param name="Name"></param>
    public Illuminated(string Name)
    {
        statusName = Name;
    }

    /// <summary>
    /// temporary for testing purposes.
    /// </summary>
    public override void Trigger()
    {
        Debug.Log("You are illuminated!");
    }  
}
