using UnityEngine;

/// <summary>
/// Governs an actor's animations. Its main use is for the player,
/// but should be adequate for enemies as well.
/// </summary>
/// <param name="animator">The actor animator</param>
/// <param name="lastX">The last "valid" player input in the X direction as it pertains to animations</param>
/// <param name="lastY">The last "valid" player input in the Y direction as it pertains to animations</param>
/// <param name="seroApproximationBoundary">The boundary value used to positively and negatively round an input to zero</param>
public class ActorAnimations : MonoBehaviour
{
    [SerializeField]
    Animator animator; // Should be set in Editor
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
    /// <param name="speed">The player speed</param>
    public void SetMovementValues(float x, float y, float speed)
    {
        animator.SetFloat(AnimationConstants.lastX, lastX);
        animator.SetFloat(AnimationConstants.lastY, lastY);

        animator.SetFloat(AnimationConstants.movingX, x);
        animator.SetFloat(AnimationConstants.movingY, y);

        CheckLastMovementInput(x, y);

        SetIsWalking(x, y);
    }

    /// <summary>
    /// Checks for there to be movement input in at least one direction and sets
    /// the values for the last inputs that were relevant for the animation logic.
    /// </summary>
    /// <param name="x">Player input in the X direction</param>
    /// <param name="y">Player input in the Y direction</param>
    void CheckLastMovementInput(float x, float y)
    {
        if ((Mathf.Abs(x) + Mathf.Abs(y)) > 0)
        {
            CheckAxisLastMovementInput(x, ref lastX);
            CheckAxisLastMovementInput(y, ref lastY);
        }
    }

    /// <summary>
    /// Checks for the <paramref name="newValue"/> to be larger than the threshold in order to round
    /// the input value and record it as the last known value for that direction.
    /// </summary>
    /// <param name="newValue"></param>
    /// <param name="lastValue"></param>
    void CheckAxisLastMovementInput(float newValue, ref float lastValue)
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
    /// Sets the boolean in the animator for whether the actor is moving or not.
    /// </summary>
    /// <param name="x">Player input in the X direction</param>
    /// <param name="y">Player input in the Y direction</param>
    void SetIsWalking(float x, float y)
    {
        animator.SetBool(AnimationConstants.isMoving, !Mathf.Approximately(x, 0f) || !Mathf.Approximately(y, 0f));
    }
}
