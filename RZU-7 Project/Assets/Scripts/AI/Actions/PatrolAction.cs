using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Actions
{
    public override void Act(StateController controller)
    {
        if(controller.patrolPoints.Length > 0)
        {
            Patrol(controller);
        }
    }

    void Patrol(StateController controller)
    {
        if(controller.patrolPoints.Length >= 2)
        {
            if (Vector2.Distance(controller.transform.position, controller.currentPatrolPoint) < .25f)
            {
                for (int i = 0; i < controller.patrolPoints.Length; i++)
                {
                    if (controller.patrolDirection == StateController.PatrolDirection.forwards)
                    {
                        if(controller.currentPatrolPoint == controller.patrolPoints[i])
                        {
                            if(i < controller.patrolPoints.Length - 1)
                            {
                                controller.currentPatrolPoint = controller.patrolPoints[i + 1];
                                break;
                            }
                            else
                            {
                                controller.patrolDirection = StateController.PatrolDirection.backwards;
                                break;
                            }
                        }
                    }
                    else if(controller.patrolDirection == StateController.PatrolDirection.backwards)
                    {
                        if(controller.currentPatrolPoint == controller.patrolPoints[i])
                        {
                            if(i > 0)
                            {
                                controller.currentPatrolPoint = controller.patrolPoints[i - 1];
                                break;
                            }
                            else
                            {
                                controller.patrolDirection = StateController.PatrolDirection.forwards;
                                break;
                            }
                        }
                    }
                }
            }
            controller.rb2d.AddForce((controller.currentPatrolPoint - (Vector2)controller.transform.position).normalized * controller.stats.walkSpeed, ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log($"{controller.gameObject.name} only has less than 2 patrol points assigned.");
        }

    }
}
