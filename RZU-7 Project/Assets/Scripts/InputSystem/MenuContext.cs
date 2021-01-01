using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interprets actions to take in menus based on user input
/// </summary>
public class MenuContext : InputContext
{
    /// <summary>
    /// Handles horizontal axis movement
    /// </summary>
    /// <param name="value">The input value for horizontal direction</param>
    public override void HorizontalButtonPress(float value)
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.horizontal + ": " + Input.GetAxis(InputConstants.horizontal));
    }

    /// <summary>
    /// Handles vertical axis movement
    /// </summary>
    /// <param name="value">The input value for vertical direction</param>
    public override void VerticalButtonPress(float value)
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.vertical + ": " + Input.GetAxis(InputConstants.vertical));
    }

    /// <summary>
    /// Handles Action1 button presses
    /// </summary>
    public override void Action1ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.action1 + ": " + Input.GetButtonDown(InputConstants.action1));
    }

    /// <summary>
    /// Handles Action2 button presses
    /// </summary>
    public override void Action2ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.action2 + ": " + Input.GetButtonDown(InputConstants.action2));
    }

    /// <summary>
    /// Handles Menu1 button presses
    /// </summary>
    public override void Menu1ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.menu1 + ": " + Input.GetButtonDown(InputConstants.menu1));
    }

    /// <summary>
    /// Handles Menu2 button presses
    /// </summary>
    public override void Menu2ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.menu2 + ": " + Input.GetButtonDown(InputConstants.menu2));
    }
}
