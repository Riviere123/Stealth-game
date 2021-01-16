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
    protected float zeroApproximationBoundary = 0.01f;
    [SerializeField]
    protected Animator animator;
    protected float lastX = 0f;
    protected float lastY = 0f;

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
        CheckImobileConditions(ref x, ref y);

        animator.SetFloat(AnimationConstants.lastX, lastX);
        animator.SetFloat(AnimationConstants.lastY, lastY);

        // If there is movement in at least one axis...
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

    public virtual void CheckImobileConditions(ref float x, ref float y)
    {
        if (animator.GetBool(AnimationConstants.isDead) || animator.GetBool(AnimationConstants.isInteracting))
        {
            x = y = 0f;
        }
    }

    /// <summary>
    /// Sets the IsDead boolean in the animator
    /// </summary>
    /// <param name="val">The boolean value to set IsDead to</param>
    public void SetIsDeadStatus(bool val)
    {
        SetActorStatus(AnimationConstants.isDead, val);
    }

    /// <summary>
    /// Sets the IsDead boolean in the animator
    /// </summary>
    /// <remarks>
    /// Sigh...This exists because AnimatorEvents do not support calling functions with bool parameters
    /// </remarks>
    /// <param name="val">The boolean value to set IsDead to</param>
    public void SetIsDeadStatus(Constants.ActorBoolState val)
    {
        SetActorStatus(AnimationConstants.isDead, val);
    }

    /// <summary>
    /// Sets the IsInteracting boolean in the animator
    /// </summary>
    /// <param name="val">The boolean value to set IsInteracting to</param>
    public void SetIsInteractingStatus(bool val)
    {
        SetActorStatus(AnimationConstants.isInteracting, val);
    }

    /// <summary>
    /// Sets the IsInteracting boolean in the animator
    /// </summary>
    /// <remarks>
    /// Sigh...This exists because AnimatorEvents do not support calling functions with bool parameters
    /// Also, there is some bug in Unity where the original overloaded name of this function caused the
    /// animation event logic to fail because it tries to use the bool version of this funcion. Hence the
    /// "Enum" at the end of the name.
    /// </remarks>
    /// <param name="val">The boolean value to set IsInteracting to</param>
    public void SetIsInteractingStatusEnum(Constants.ActorBoolState val)
    {
        SetActorStatus(AnimationConstants.isInteracting, val);
    }

    /// <summary>
    /// Sets a boolean type in the animator
    /// </summary>
    /// <param name="status">The name of the variable to set in the animator</param>
    /// <param name="val">The value to set the boolean to</param>
    protected void SetActorStatus(string status, bool val)
    {
        if (val)
        {
            SetMovementBooleans(MovementConstants.ActorMovementStates.NONE);
        }

        animator.SetBool(status, val);
    }

    /// <summary>
    /// Sets a boolean type in the animator
    /// </summary>
    /// <param name="status">The name of the variable to set in the animator</param>
    /// <param name="val">The value to set the boolean to</param>
    /// /// <remarks>
    /// Sigh...This exists because AnimatorEvents do not support calling functions with bool parameters
    /// </remarks>
    protected void SetActorStatus(string status, Constants.ActorBoolState val)
    {
        switch (val)
        {
            case Constants.ActorBoolState.TRUE:
                SetMovementBooleans(MovementConstants.ActorMovementStates.NONE);
                animator.SetBool(status, true);
                break;
            case Constants.ActorBoolState.FALSE:
                animator.SetBool(status, false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Checks for the <paramref name="newValue"/> to be larger than the threshold in order to round
    /// the input value and record it as the last known value for that direction.
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="lastValue"></param>
    protected void SetAxisLastMovementInput(float newValue, ref float lastValue)
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
    public virtual void SetMovementBooleans(MovementConstants.ActorMovementStates movementState)
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
