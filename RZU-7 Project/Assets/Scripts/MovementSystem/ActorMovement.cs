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
    InputConstants.ActorMovementStates currentMoveState;
    float currentSpeed;

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

    void SetSpeed(InputConstants.ActorMovementStates state)
    {
        switch(state)
        {
            case InputConstants.ActorMovementStates.normal:
                currentSpeed = normalSpeed;
                currentMoveState = state;
                break;

            case InputConstants.ActorMovementStates.sprint:
                currentSpeed = sprintSpeed;
                currentMoveState = state;
                break;

            case InputConstants.ActorMovementStates.crouch:
                currentSpeed = crouchSpeed;
                currentMoveState = state;
                break;

            default:
                break;
        }
    }

    void ToggleState(InputConstants.ActorMovementStates newState)
    {
        if (currentMoveState != newState)
        {
            SetSpeed(newState);
        }
        else
        {
            SetSpeed(InputConstants.ActorMovementStates.normal);
        }
    }

    public void MoveActor(float horizontal, float vertical)
    {
        Debug.Log("Force Value: " + new Vector2(horizontal, vertical).normalized * currentSpeed * speedModifier * Time.fixedDeltaTime);
        rigidBody.AddForce(new Vector2(horizontal, vertical).normalized * currentSpeed * speedModifier * Time.fixedDeltaTime);
    }

    public void ToggleCrouch()
    {
        ToggleState(InputConstants.ActorMovementStates.crouch);
    }

    public void ToggleSprint()
    {
        ToggleState(InputConstants.ActorMovementStates.sprint);
    }

    public void SetSpeedModifier(float value)
    {
        speedModifier = Mathf.Clamp(value, minModifier, maxModifier);
    }
}
