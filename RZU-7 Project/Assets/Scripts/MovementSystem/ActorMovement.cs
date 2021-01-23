using UnityEngine;

/// <summary>
/// Handles actor physiscs based movement
/// </summary>
/// <param name="rigidBody">The actor's RigidBody component</param>
/// <param name="normalSpeed">The actor's normal speed</param>
/// <param name="sprintSpeed">The actor's sprint speed</param>
/// <param name="crouchSpeed">The actor's crouch speed</param>
/// <param name="currentMoveState">The actor's current move state</param>
/// <param name="currentSpeed">The actor's current speed as determined by the <paramref name="currentMoveState"/> parameter</param>
/// <param name="speedValueMultiplier">A static multiplier to reduce the size of the speed values in the editor</param>
/// <param name="speedModifier">The speed modifier to apply to the actor speed
/// depending on external conditions or special statuses</param>
/// <param name="maxModifier">The ceiling for the <paramref name="speedModifier"/> parameter</param>
/// <param name="minModifier">The floor for the <paramref name="speedModifier"/> parameter</param>
/// <param name="cantMove">The can't move flag to stop all movement</param>
/// <param name="CantMove">The property to get/set the <paramref name="cantMove"/> parameter</param>
[RequireComponent(typeof(Rigidbody2D))]
public class ActorMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody;
    [SerializeField]
    float normalSpeed = 12f;
    [SerializeField]
    float sprintSpeed = 28f;
    [SerializeField]
    float crouchSpeed = 8f;
    public MovementConstants.ActorMovementStates currentMoveState;
    float currentSpeed;
    float speedValueMultiplier = 10f;  // Because the values get too high in the editor

    // speedModifier is meant to affect speed values based on environment or status conditions
    float speedModifier = 1f;
    [SerializeField]
    float maxModifier = 1.4f;
    [SerializeField]
    float minModifier = .5f;
    [SerializeField]
    bool cantMove;
    public bool CantMove
    {
        get { return cantMove; }
        set { cantMove = value; }
    }

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    /// <summary>
    /// Sets the <paramref name="currentSpeed"/> and <paramref name="currentMoveState"/> variables
    /// based on the move state passed in
    /// </summary>
    /// <param name="state">The state to set the <paramref name="currentMoveState"/> parameter to</param>
    void SetSpeed(MovementConstants.ActorMovementStates state)
    {
        switch(state)
        {
            case MovementConstants.ActorMovementStates.WALK:
                currentSpeed = normalSpeed;
                currentMoveState = state;
                break;

            case MovementConstants.ActorMovementStates.RUN:
                currentSpeed = sprintSpeed;
                currentMoveState = state;
                break;

            case MovementConstants.ActorMovementStates.CROUCH:
                currentSpeed = crouchSpeed;
                currentMoveState = state;
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Sets the <paramref name="currentMoveState"/> parameter to <paramref name="newState"/> if not on it.
    /// Otherwise it toggles to the "WALK" state.
    /// </summary>
    /// <param name="newState">The new state to set <paramref name="currentMoveState"/> to</param>
    void ToggleState(MovementConstants.ActorMovementStates newState)
    {
        if (Input.GetButton(InputConstants.action1) || Input.GetButton(InputConstants.action2))
        {
            SetSpeed(newState);
        }
        else
        {
            SetSpeed(MovementConstants.ActorMovementStates.WALK);
        }
    }

    /// <summary>
    /// Moves the actor based on user input
    /// </summary>
    /// <param name="horizontal">The X component of the movement vector</param>
    /// <param name="vertical">The Y component of the movement vector</param>
    public void MoveActor(float horizontal, float vertical)
    {
        if (!cantMove)
        {
            rigidBody.AddForce(new Vector2(horizontal, vertical).normalized * currentSpeed * speedValueMultiplier * speedModifier * Time.fixedDeltaTime);
        }
        else
        {
            rigidBody.AddForce(Vector2.zero);
        }
    }

    /// <summary>
    /// Toggles <paramref name="currentMoveState"/> from "CROUCH" to "WALK" and
    /// viceversa based on the value of <paramref name="currentMoveState"/>
    /// </summary>
    public void ToggleCrouch()
    {
        ToggleState(MovementConstants.ActorMovementStates.CROUCH);
    }

    /// <summary>
    /// Toggles <paramref name="currentMoveState"/> from "RUN" to "WALK" and
    /// viceversa based on the value of <paramref name="currentMoveState"/>
    /// </summary>
    public void ToggleSprint()
    {
        ToggleState(MovementConstants.ActorMovementStates.RUN);
    }

    /// <summary>
    /// Sets the speed modifier for the actor
    /// </summary>
    /// <param name="value">The value to set the speed modifier to</param>
    public void SetSpeedModifier(float value)
    {
        speedModifier = Mathf.Clamp(value, minModifier, maxModifier);
    }

    /// <summary>
    /// Gets the <paramref name="currentMoveState"/> value
    /// </summary>
    /// <returns>The <paramref name="currentMoveState"/> value</returns>
    public MovementConstants.ActorMovementStates GetMoveState()
    {
        return currentMoveState;
    }

    /// <summary>
    /// Sets the <paramref name="cantMove"/> parameter
    /// </summary>
    /// <param name="val">The value to set <paramref name="cantMove"/> to</param>
    public void SetCantMove(Constants.ActorBoolState val)
    {
        switch (val)
        {
            case Constants.ActorBoolState.TRUE:
                cantMove = true;
                break;
            case Constants.ActorBoolState.FALSE:
                cantMove = false;
                break;
            default:
                break;
        }
    }
}
