using UnityEngine;

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
    [SerializeField]
    MovementConstants.ActorMovementStates currentMoveState;
    float currentSpeed;
    float speedValueMultiplier = 10f;  // Because the values get too high in the editor

    // speedModifier is meant to affect speed values based on environment or status conditions
    float speedModifier = 1f;
    [SerializeField]
    float maxModifier = 1.4f;
    [SerializeField]
    float minModifier = .5f;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

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

    void ToggleState(MovementConstants.ActorMovementStates newState)
    {
        if (currentMoveState != newState)
        {
            SetSpeed(newState);
        }
        else
        {
            SetSpeed(MovementConstants.ActorMovementStates.WALK);
        }
    }

    public void MoveActor(float horizontal, float vertical)
    {
        Debug.Log("Force Value: " + new Vector2(horizontal, vertical).normalized * currentSpeed * speedValueMultiplier * speedModifier * Time.fixedDeltaTime);
        rigidBody.AddForce(new Vector2(horizontal, vertical).normalized * currentSpeed * speedValueMultiplier * speedModifier * Time.fixedDeltaTime);
    }

    public void ToggleCrouch()
    {
        ToggleState(MovementConstants.ActorMovementStates.CROUCH);
    }

    public void ToggleSprint()
    {
        ToggleState(MovementConstants.ActorMovementStates.RUN);
    }

    public void SetSpeedModifier(float value)
    {
        speedModifier = Mathf.Clamp(value, minModifier, maxModifier);
    }

    public MovementConstants.ActorMovementStates GetMoveState()
    {
        return currentMoveState;
    }
}
