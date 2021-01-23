using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/Listen")]
public class ListenDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Listen(controller);
    }

    bool Listen(StateController controller)
    {
        if (controller.heardPosition != Vector2.zero)
        {
            controller.GetHelper<MovementHelper>().path.Clear();
            return true;
        }
        else
        {
            return false;
        }
    }
}
