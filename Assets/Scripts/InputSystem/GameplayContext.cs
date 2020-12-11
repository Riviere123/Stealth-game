using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameplayContext : InputContext
{
    public override void HorizontalButtonPress(float value)
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.horizontal + ": " + Input.GetAxis(Constants.horizontal));
    }

    public override void VerticalButtonPress(float value)
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.vertical + ": " + Input.GetAxis(Constants.vertical));
    }

    public override void Action1ButtonPress()
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.action1 + ": " + Input.GetButtonDown(Constants.action1));
    }

    public override void Action2ButtonPress()
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.action2 + ": " + Input.GetButtonDown(Constants.action2));
    }

    public override void Menu1ButtonPress()
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.menu1 + ": " + Input.GetButtonDown(Constants.menu1));
    }

    public override void Menu2ButtonPress()
    {
        Debug.Log(Constants.gameplayContext + " " + Constants.input + " " + Constants.menu2 + ": " + Input.GetButtonDown(Constants.menu2));
    }
}
