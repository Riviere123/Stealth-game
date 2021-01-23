using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Actions
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    void Chase(StateController controller)
    {
        MovementHelper moveHelper = controller.GetHelper<MovementHelper>();
        EnemyVisualCone vision = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone);
        EnemyAnimations animations = controller.references.Get<EnemyAnimations>(EnemyReferencesConstants.animations);
        animations.SetMovementBooleans(MovementConstants.ActorMovementStates.WALK);
        animations.SetImobileBools(AnimationConstants.ImobileStates.NONE);
        animations.SetIsAttackingStatus(false);

        if (vision.target)
        {
            RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, vision.target.transform.position - controller.transform.position, LayerMask.NameToLayer("Player"));
            if (hit)
            {
                controller.references.Get<Rigidbody2D>(EnemyReferencesConstants.rigidBody).AddForce((vision.target.transform.position - controller.transform.position).normalized * controller.references.Get<AIStats>("stats").runSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                if (moveHelper.path.Count <= 0)
                {
                    FindNewPath(controller);
                    return;
                }
                else if (Vector2.Distance(vision.target.transform.position, moveHelper.path[moveHelper.path.Count - 1].position) > AIConstants.TargetDistanceToFindNewPath)
                {
                    FindNewPath(controller);
                    return;
                }
                else if (Vector2.Distance(controller.transform.position, moveHelper.path[0].position) < AIConstants.DistanceToRemovePoint)
                {
                    moveHelper.path.RemoveAt(0);
                    return;
                }
                controller.references.Get<Rigidbody2D>(EnemyReferencesConstants.rigidBody).AddForce((moveHelper.path[0].position - controller.transform.position).normalized * controller.references.Get<AIStats>("stats").runSpeed * Time.deltaTime, ForceMode2D.Impulse);

            }

        }
    }

    void FindNewPath(StateController controller)
    {
        MovementHelper moveHelper = controller.GetHelper<MovementHelper>();
        GameObject target = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone).target;
        moveHelper.path = moveHelper.pathFinding.FindPath(controller.transform.position, target.transform.position );
    }
}


