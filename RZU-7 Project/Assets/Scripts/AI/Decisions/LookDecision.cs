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
        RaycastHit2D hit = Physics2D.Raycast(controller.transform.position, Vector2.right);
        Debug.DrawRay(controller.transform.position, Vector2.right * 20);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
