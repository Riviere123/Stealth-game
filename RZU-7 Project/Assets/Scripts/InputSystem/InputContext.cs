using UnityEngine;

/// <summary>
/// Abstract parent to enforce the definition of all input functions for each context
/// </summary>
public abstract class InputContext : MonoBehaviour
{
    /// <summary>
    /// Handles horizontal axis movement
    /// </summary>
    /// <param name="value">The input value for horizontal direction</param>
    public abstract void HorizontalButtonPress(float value);

    /// <summary>
    /// Handles vertical axis movement
    /// </summary>
    /// <param name="value">The input value for vertical direction</param>
    public abstract void VerticalButtonPress(float value);

    /// <summary>
    /// Handles Action1 button presses
    /// </summary>
    public abstract void Action1ButtonPress();

    /// <summary>
    /// Handles Action2 button presses
    /// </summary>
    public abstract void Action2ButtonPress();

    /// <summary>
    /// Handles Menu1 button presses
    /// </summary>
    public abstract void Menu1ButtonPress();

    /// <summary>
    /// Handles Menu2 button presses
    /// </summary>
    public abstract void Menu2ButtonPress();
}
