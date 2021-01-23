using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Actions
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    void Attack(StateController controller)
    {
        Animator animator = controller.references.Get<Animator>(EnemyReferencesConstants.animator);
        EnemyAnimations animations = controller.references.Get<EnemyAnimations>(EnemyReferencesConstants.animations);
        EnemyVisualCone vision = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone);

        if (vision.target)
        {
            animations.SetMovementBooleans(MovementConstants.ActorMovementStates.NONE);
            Vector2 attackDirection = (vision.target.transform.position - controller.gameObject.transform.position).normalized;
            animator.SetFloat(AnimationConstants.lastX, attackDirection.x);
            animator.SetFloat(AnimationConstants.lastY, attackDirection.y);
            controller.GetHelper<MovementHelper>().Attack(animations);
        }    
    }
}


