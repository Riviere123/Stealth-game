using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Actions
{
    public override void Act(StateController controller)
    {
        MovementHelper moveHelper = (MovementHelper)controller.GetHelper(AIConstants.movementHelperKey);
        if (moveHelper.patrolPoints.Length > 0)
        {
            Patrol(controller);
        }
    }

    void Patrol(StateController controller)
    {
        MovementHelper moveHelper = (MovementHelper)controller.GetHelper(AIConstants.movementHelperKey);
        GeneralHelper generalHelper = (GeneralHelper)controller.GetHelper(AIConstants.generalHelperKey);
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, moveHelper.currentPatrolPoint - (Vector2)controller.transform.position,Vector2.Distance(controller.transform.position,moveHelper.currentPatrolPoint),LayerMask.GetMask("Obstacle"));
        Debug.DrawRay(controller.transform.position, moveHelper.currentPatrolPoint - (Vector2)controller.transform.position);
        if (!hit)
        {
            moveHelper.path.Clear();
        }

        if (hit)
        {
            if(moveHelper.path.Count <= 0)
            {
                moveHelper.path = moveHelper.pathFinding.FindPath(controller.transform.position, moveHelper.currentPatrolPoint);
            }
            if (moveHelper.path.Count > 0)
            {
                if(Vector2.Distance(controller.transform.position,moveHelper.path[0].position) < AIConstants.DistanceToRemovePoint)
                {
                    moveHelper.path.RemoveAt(0);
                    return;
                }
                generalHelper.rigidBody.AddForce((moveHelper.path[0].position - controller.transform.position).normalized * generalHelper.stats.walkSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        
        else if(moveHelper.patrolPoints.Length >= 2)
        {
            if (!moveHelper.patrolLoop)
            {
                if (Vector2.Distance(controller.transform.position, moveHelper.currentPatrolPoint) < .5f)
                {
                    for (int i = 0; i < moveHelper.patrolPoints.Length; i++)
                    {
                        if (moveHelper.patrolDirection == AIConstants.PatrolCycleDirection.forwards)
                        {
                            if (moveHelper.currentPatrolPoint == moveHelper.patrolPoints[i])
                            {
                                if (i < moveHelper.patrolPoints.Length - 1)
                                {
                                    moveHelper.currentPatrolPoint = moveHelper.patrolPoints[i + 1];
                                    break;
                                }
                                else
                                {
                                    moveHelper.patrolDirection = AIConstants.PatrolCycleDirection.backwards;
                                    break;
                                }
                            }
                        }
                        else if (moveHelper.patrolDirection == AIConstants.PatrolCycleDirection.backwards)
                        {
                            if (moveHelper.currentPatrolPoint == moveHelper.patrolPoints[i])
                            {
                                if (i > 0)
                                {
                                    moveHelper.currentPatrolPoint = moveHelper.patrolPoints[i - 1];
                                    break;
                                }
                                else
                                {
                                    moveHelper.patrolDirection = AIConstants.PatrolCycleDirection.forwards;
                                    break;
                                }
                            }
                        }
                    }
                }
                generalHelper.rigidBody.AddForce((moveHelper.currentPatrolPoint - (Vector2)controller.transform.position).normalized * generalHelper.stats.walkSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                if (Vector2.Distance(controller.transform.position, moveHelper.currentPatrolPoint) < .5f)
                {
                    for (int i = 0; i < moveHelper.patrolPoints.Length; i++)
                    {
                        if (moveHelper.currentPatrolPoint == moveHelper.patrolPoints[i])
                        {
                            if (i == moveHelper.patrolPoints.Length - 1)
                            {
                                moveHelper.currentPatrolPoint = moveHelper.patrolPoints[0];
                                break;
                            }
                            else
                            {
                                moveHelper.currentPatrolPoint = moveHelper.patrolPoints[i + 1];
                                break;
                            }
                        }
                    }
                }
                generalHelper.rigidBody.AddForce((moveHelper.currentPatrolPoint - (Vector2)controller.transform.position).normalized * generalHelper.stats.walkSpeed * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.Log($"{controller.gameObject.name} only has less than 2 patrol points assigned.");
        }

    }
}
