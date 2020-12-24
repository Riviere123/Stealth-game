using UnityEngine;

/// <summary>
/// Governs an actor's animations. Its main use is for the player,
/// but should be adequate for enemies as well.
/// </summary>
/// <remarks>
/// The <paramref name="animator"/> parameter is meant to be set in the editor.
/// </remarks>
/// <param name="animator">The actor animator</param>
/// <param name="lastX">The last "valid" player input in the X direction as it pertains to animations</param>
/// <param name="lastY">The last "valid" player input in the Y direction as it pertains to animations</param>
/// <param name="seroApproximationBoundary">The boundary value used to positively and negatively round an input to zero</param>
public class ActorAnimations : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    float lastX = 0f;
    float lastY = 0f;
    float zeroApproximationBoundary = 0.01f;


    void Start()
    {
        if (animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }

    /// <summary>
    /// Sets the movement values in the animator.
    /// </summary>
    /// <param name="x">Player input in the X direction</param>
    /// <param name="y">Player input in the Y direction</param>
    /// <param name="movementState">The actor's movement state</param>
    public void SetMovementValues(float x, float y, MovementConstants.ActorMovementStates movementState)
    {
        animator.SetFloat(AnimationConstants.lastX, lastX);
        animator.SetFloat(AnimationConstants.lastY, lastY);

        animator.SetFloat(AnimationConstants.movingX, x);
        animator.SetFloat(AnimationConstants.movingY, y);

        if ((Mathf.Abs(x) + Mathf.Abs(y)) > 0)
        {
            SetAxisLastMovementInput(x, ref lastX);
            SetAxisLastMovementInput(y, ref lastY);
            SetMovementBooleans(movementState);
        }
        else
        {
            SetMovementBooleans(MovementConstants.ActorMovementStates.NONE);
        }
    }

    /// <summary>
    /// Checks for the <paramref name="newValue"/> to be larger than the threshold in order to round
    /// the input value and record it as the last known value for that direction.
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="lastValue"></param>
    void SetAxisLastMovementInput(float newValue, ref float lastValue)
    {
        if (Mathf.Abs(newValue) > zeroApproximationBoundary)
        {
            lastValue = newValue > 0f ? 1f : -1f;
        }
        else
        {
            lastValue = 0f;
        }
    }

    /// <summary>
    /// Sets the booleans in the animator for whether the actor is moving or not and what movement
    /// state the actor is in.
    /// </summary>
    /// <param name="movementState">The actor's movement state</param>
    void SetMovementBooleans(MovementConstants.ActorMovementStates movementState)
    {
        switch (movementState)
        {
            case MovementConstants.ActorMovementStates.WALK:
                animator.SetBool(AnimationConstants.isWalking, true);
                animator.SetBool(AnimationConstants.isRunning, false);
                animator.SetBool(AnimationConstants.isCrouching, false);
                break;
            case MovementConstants.ActorMovementStates.RUN:
                animator.SetBool(AnimationConstants.isWalking, false);
                animator.SetBool(AnimationConstants.isRunning, true);
                animator.SetBool(AnimationConstants.isCrouching, false);
                break;
            case MovementConstants.ActorMovementStates.CROUCH:
                animator.SetBool(AnimationConstants.isWalking, false);
                animator.SetBool(AnimationConstants.isRunning, false);
                animator.SetBool(AnimationConstants.isCrouching, true);
                break;
            case MovementConstants.ActorMovementStates.NONE:
                animator.SetBool(AnimationConstants.isWalking, false);
                animator.SetBool(AnimationConstants.isRunning, false);
                animator.SetBool(AnimationConstants.isCrouching, false);
                break;
            default:
                break;
        }
    }
}
