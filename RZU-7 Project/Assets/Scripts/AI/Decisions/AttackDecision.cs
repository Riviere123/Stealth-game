using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decision/Attack")]
public class AttackDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        EnemyVisualCone vision = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone);
        if (vision.target)
        {
            return Mathf.Abs(Vector2.Distance(vision.target.transform.position, controller.gameObject.transform.position)) < AIConstants.DistanceToAttack;
        }
        else
        {
            return false;
        }
    }
}
