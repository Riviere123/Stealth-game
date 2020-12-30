using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This holds a list of statuseffects for accessing what status's the game object is effected by.
/// </summary>
public class Status : MonoBehaviour
{
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
}
