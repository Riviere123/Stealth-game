using UnityEngine;

/// <summary>
/// Abstracts the logic to implement for when the player interacts with an object
/// </summary>
public abstract class Interaction : MonoBehaviour
{
    /// <summary>
    /// Do logic when the player interacts with item
    /// </summary>
    /// <param name="isOn">Whether or not the item is in the "On" state</param>
    public abstract void OnInteract(bool isOn);
}
