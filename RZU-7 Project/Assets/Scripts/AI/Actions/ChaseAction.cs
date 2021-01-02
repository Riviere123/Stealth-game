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
            controller.rb2d.AddForce((controller.vision.target.transform.position - controller.transform.position).normalized * controller.stats.runSpeed, ForceMode2D.Impulse);
        }
    }
}
