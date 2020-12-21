using UnityEngine;
using UnityEditor.Animations;


public class ActorAnimations : MonoBehaviour
{
    [SerializeField]
    AnimatorController animator; // Should be set in Editor

    void Start()
    {
        animator = gameObject.GetComponent<AnimatorController>();
    }

    public void SetMovementValues(float x, float y, float speed)
    {
        // Set animator movement variables here
    }
}
