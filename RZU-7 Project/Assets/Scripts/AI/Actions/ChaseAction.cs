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
        if (controller.vision.target)
        {
            if(controller.path.Count <= 0)
            {
                FindNewPath(controller);
                return;
            }
            else if(Vector2.Distance(controller.vision.target.transform.position ,controller.path[controller.path.Count-1].position) > ChaseActionConstants.TargetDistanceToFindNewPath)
            {
                FindNewPath(controller);
                return;
            }
            else if (Vector2.Distance(controller.transform.position, controller.path[0].position) < ChaseActionConstants.DistanceToRemovePoint)
            {
                controller.path.RemoveAt(0);
                return;
            }
            controller.rb2d.AddForce((controller.path[0].position - controller.transform.position).normalized * controller.stats.runSpeed, ForceMode2D.Impulse);
        }
    }

    void FindNewPath(StateController controller)
    {
        controller.path = controller.pathFinding.FindPath(controller.transform.position, controller.vision.target.transform.position);
    }
}


