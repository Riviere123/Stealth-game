using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Investigate")]
public class InvestigateAction : Actions
{
    public override void Act(StateController controller)
    {
        Investigate(controller);
    }
    void Investigate(StateController controller)
    {
        MovementHelper moveHelper = controller.GetHelper<MovementHelper>();
        

        if(moveHelper.path.Count <= 0 || controller.heardPosition != Vector2.zero)
        {
            FindNewPath(controller);
        }


        if (moveHelper.path.Count > 0)
        {
            if (Vector2.Distance(controller.transform.position, moveHelper.path[0].position) < AIConstants.DistanceToRemovePoint)
            {
                moveHelper.path.RemoveAt(0);
                return;
            }

            controller.references.Get<Rigidbody2D>(EnemyReferencesConstants.rigidBody).AddForce((moveHelper.path[0].position - controller.transform.position).normalized * controller.references.Get<AIStats>("stats").walkSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    void FindNewPath(StateController controller)
    {
        MovementHelper moveHelper = controller.GetHelper<MovementHelper>();
        GameObject target = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone).target;
        moveHelper.path = moveHelper.pathFinding.FindPath(controller.transform.position, controller.heardPosition);
        controller.heardPosition = Vector2.zero;
    }
}
