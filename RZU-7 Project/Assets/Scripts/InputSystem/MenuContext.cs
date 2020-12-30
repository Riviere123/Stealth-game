using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuContext : InputContext
{
    public override void HorizontalButtonPress(float value)
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.horizontal + ": " + Input.GetAxis(InputConstants.horizontal));
    }

    public override void VerticalButtonPress(float value)
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.vertical + ": " + Input.GetAxis(InputConstants.vertical));
    }

    public override void Action1ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.action1 + ": " + Input.GetButtonDown(InputConstants.action1));
    }

    public override void Action2ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.action2 + ": " + Input.GetButtonDown(InputConstants.action2));
    }

    public override void Menu1ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.menu1 + ": " + Input.GetButtonDown(InputConstants.menu1));
    }

    public override void Menu2ButtonPress()
    {
        Debug.Log(InputConstants.menuContext + " " + InputConstants.input + " " + InputConstants.menu2 + ": " + Input.GetButtonDown(InputConstants.menu2));
    }
}
