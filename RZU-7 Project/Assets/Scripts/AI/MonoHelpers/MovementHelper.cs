using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : StateMonoHelper
{
    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public PathFinding pathFinding;
    [HideInInspector]
    public bool patrolLoop;
    public Vector2[] patrolPoints;

    [HideInInspector]
    public Vector2 currentPatrolPoint;
    [HideInInspector]
    public AIConstants.PatrolCycleDirection patrolDirection;
    [HideInInspector]
    public List<Node> path;

    [SerializeField]
    Color wayPointGizmoColor;
    [SerializeField]
    Color pathNodeColor;

    EnemyVisualCone vision;

    public bool GetPatrolLoop()
    {
        return patrolLoop;
    }

    public void GetPatrolLoop(bool state)
    {
        patrolLoop = state;
    }

    private void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolPoint = patrolPoints[0];
        }
        vision = controller.references.Get<EnemyVisualCone>(EnemyReferencesConstants.visualCone);

        grid = GameObject.FindGameObjectWithTag(Constants.gameMaster).GetComponent<Grid>();
        pathFinding = grid.pathFinding;

    }
    private void Update()
    {
        SnapConeDirection();
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.color = wayPointGizmoColor;
            Gizmos.DrawWireSphere(patrolPoints[i], .25f);
        }
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.color = pathNodeColor;
            Gizmos.DrawWireSphere(path[i].position, .33f);
        }
    }

    void SnapConeDirection()
    {
        Animator animator = controller.references.Get<Animator>(EnemyReferencesConstants.animator);
        GameObject visualCone = controller.references.Get<GameObject>(EnemyReferencesConstants.visualConeGameObject);

        if (!vision.target)
        {
            float x = animator.GetFloat(AnimationConstants.lastX);
            float y = animator.GetFloat(AnimationConstants.lastY);
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            visualCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 dir = vision.target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            visualCone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
