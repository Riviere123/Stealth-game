using UnityEngine;

/// <summary>
/// Attach this to any object that you want to give gold upon pickup.
/// </summary>
/// <param name="levelLogic">This should reference the levelLogic script on the scene that this script is in.</param>
/// <param name="value">The value of the object.</param>
public class Valuable : MonoBehaviour
{
    public int value = 0;
}
