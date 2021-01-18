using UnityEngine;

/// <summary>
/// Holds values relevant to an actor controlled by the AI system
/// </summary>
/// <param name="walkSpeed">The walking speed for the actor</param>
/// <param name="runSpeed">The running speed for the actor</param>
[CreateAssetMenu(menuName ="AI/Stats")]
public class AIStats : ScriptableObject
{
    [Range(1,100)]
    public float walkSpeed;
    [Range(1, 100)]
    public float runSpeed;
}
