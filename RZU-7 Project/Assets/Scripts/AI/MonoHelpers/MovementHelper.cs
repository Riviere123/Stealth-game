using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : StateMonoHelper
{
    [Header("Movement")]
    public Grid grid;

    [HideInInspector]
    public PathFinding pathFinding;
    [SerializeField] // DEBUG
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

    [HideInInspector]
    public EnemyVisualCone vision;

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
        vision = GetComponentInChildren<EnemyVisualCone>();

        grid = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Grid>();
        pathFinding = grid.pathFinding;

    }
    private void Update()
    {
        RotateToMovement();
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

    void RotateToMovement()
    {
        if (!vision.target)
        {
            Vector3 dir = GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 dir = vision.target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}
