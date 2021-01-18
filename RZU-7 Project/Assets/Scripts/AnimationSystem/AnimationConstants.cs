/// <summary>
/// Collects all constants related to the animation system
/// </summary>
public class AnimationConstants
{
    public static readonly string lastX = "LastX";
    public static readonly string lastY = "LastY";
    public static readonly string isWalking = "IsWalking";
    public static readonly string isRunning = "IsRunning";
    public static readonly string isCrouching = "IsCrouching";
    public static readonly string isDead = "IsDead";
    public static readonly string isInteracting = "IsInteracting";
    public static readonly string isAttacking = "IsAttacking";

    public enum ImobileStates { DEAD, INTERACT, ATTACK, NONE }
}
