using UnityEngine;


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

    public void SetMovementValues(float x, float y, float speed)
    {
        animator.SetFloat(AnimationConstants.lastX, lastX);
        animator.SetFloat(AnimationConstants.lastY, lastY);

        animator.SetFloat(AnimationConstants.movingX, x);
        animator.SetFloat(AnimationConstants.movingY, y);

        CheckLastMovementInput(x, y);

        SetIsMoving(x, y);
    }

    void CheckLastMovementInput(float x, float y)
    {
        if ((Mathf.Abs(x) + Mathf.Abs(y)) > 0)
        {
            CheckAxisLastMovementInput(x, ref lastX);
            CheckAxisLastMovementInput(y, ref lastY);
        }
    }

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

    void SetIsMoving(float x, float y)
    {
        animator.SetBool(AnimationConstants.isMoving, !Mathf.Approximately(x, 0f) || !Mathf.Approximately(y, 0f));
    }
}
