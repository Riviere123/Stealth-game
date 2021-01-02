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
        Debug.Log("I'm Patrolling!");
    }
}
