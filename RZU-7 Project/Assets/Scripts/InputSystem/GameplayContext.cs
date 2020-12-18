using UnityEngine;

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

    void Update()
    {
        actorAnimations.SetMovementValues(horizontalValue, verticalValue, new Vector2(horizontalValue, verticalValue).sqrMagnitude);
    }

    // Move in the X plane
    public override void HorizontalButtonPress(float value)
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.horizontal + ": " + Input.GetAxis(InputConstants.horizontal));
        horizontalValue = value;
    }

    // Move in the Y plane
    public override void VerticalButtonPress(float value)
    {
        //Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.vertical + ": " + Input.GetAxis(InputConstants.vertical));
        verticalValue = value;
    }

    // Toggle crouch
    public override void Action1ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.action1 + ": " + Input.GetButtonDown(InputConstants.action1));
        actorMovement.ToggleCrouch();
    }

    // Toggle sprint
    public override void Action2ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.action2 + ": " + Input.GetButtonDown(InputConstants.action2));
        actorMovement.ToggleSprint();
    }

    // Pause menu
    public override void Menu1ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.menu1 + ": " + Input.GetButtonDown(InputConstants.menu1));
    }

    // Toggle map
    public override void Menu2ButtonPress()
    {
        Debug.Log(InputConstants.gameplayContext + " " + InputConstants.input + " " + InputConstants.menu2 + ": " + Input.GetButtonDown(InputConstants.menu2));
    }

}

