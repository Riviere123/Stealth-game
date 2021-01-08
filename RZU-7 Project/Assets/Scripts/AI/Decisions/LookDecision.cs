using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/Look")]
public class LookDecision : Decision
{
    
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    bool Look(StateController controller)
    {

        if(((MovementHelper)controller.GetHelper(AIConstants.movementHelperKey)).vision.target != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
