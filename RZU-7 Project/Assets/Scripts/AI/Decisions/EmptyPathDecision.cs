using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/EmptyPath")]
public class EmptyPathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return EmpyPath(controller);
    }

    bool EmpyPath(StateController controller)
    {
        if(controller.GetHelper<MovementHelper>().path.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
