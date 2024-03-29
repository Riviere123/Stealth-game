﻿using UnityEngine;

/// <summary>
/// Interprets user input as gameplay input
/// </summary>
/// <param name="actorMovement">The ActorMovement script component for the player actor</param>
/// <param name="actorAnimations">The ActorAnimations script component for the player actor</param>
/// <param name="horizontalValue">The last recorded horizontal axis input</param>
/// <param name="verticalValue">The last recorded vertical axis input</param>
public class GameplayContext : InputContext
{
    [SerializeField]
    ActorMovement actorMovement;      // Should be set in Editor from Player Object
    [SerializeField]
    ActorAnimations actorAnimations;  // Should be set in Editor from Player Object
    float horizontalValue, verticalValue = 0f;

    // Handle physics based movement
    void FixedUpdate()
    {
        actorMovement.MoveActor(horizontalValue, verticalValue);
    }

    // Handle animation updates
    void Update()
    {
        actorAnimations.SetMovementValues(horizontalValue, verticalValue, actorMovement.GetMoveState());
    }

    /// <summary>
    /// Move in the X plane
    /// </summary>
    /// <param name="value">The horizontal movement value</param>
    public override void HorizontalButtonPress(float value)
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.horizontal + ": " + Input.GetAxis(InputConstants.horizontal));
        horizontalValue = value;
    }

    /// <summary>
    /// Move in the Y plane
    /// </summary>
    /// <param name="value">The vertical movement value</param>
    public override void VerticalButtonPress(float value)
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.vertical + ": " + Input.GetAxis(InputConstants.vertical));
        verticalValue = value;
    }

    /// <summary>
    /// Toggle crouch
    /// </summary>
    public override void Action1ButtonPress()
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.action1 + ": " + Input.GetButtonDown(InputConstants.action1));
        actorMovement.ToggleCrouch();
    }

    /// <summary>
    /// Toggle sprint
    /// </summary>
    public override void Action2ButtonPress()
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.action2 + ": " + Input.GetButtonDown(InputConstants.action2));
        actorMovement.ToggleSprint();
    }

    /// <summary>
    /// Pause menu
    /// </summary>
    public override void Menu1ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.menu1 + ": " + Input.GetButtonDown(InputConstants.menu1));
    }

    /// <summary>
    /// Toggle map
    /// </summary>
    public override void Menu2ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.menu2 + ": " + Input.GetButtonDown(InputConstants.menu2));
    }

}

