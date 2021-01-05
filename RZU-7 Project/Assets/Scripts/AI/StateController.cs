using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StateController : MonoBehaviour
{
    public Grid grid;

    public bool aiActive;
    public State currentState;
    public AIStats stats;

    [HideInInspector]
    public PathFinding pathFinding;
    public bool patrolLoop;
    public Vector2[] patrolPoints;

    [HideInInspector]
    public Vector2 currentPatrolPoint;
    [HideInInspector]
    public PatrolDirection patrolDirection;
    [HideInInspector]
    public List<Node> path;

    [SerializeField]
    Color wayPointGizmoColor;
    [SerializeField]
    Color pathNodeColor;

    [HideInInspector]
    public Rigidbody2D rb2d;
    [HideInInspector]
    public EnemyVisualCone vision;

    private void Awake()
    {
        if(patrolPoints.Length > 0)
        {
            currentPatrolPoint = patrolPoints[0];
        }
        vision = GetComponentInChildren<EnemyVisualCone>();
        rb2d = GetComponent<Rigidbody2D>();
        
        grid = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Grid>();
        pathFinding = grid.pathFinding;
    }
    private void Update()
    {
        if (!aiActive)
        {
            return;
        }
        
        currentState.UpdateState(this);
        RotateToMovement();
    }

    private void OnDrawGizmosSelected()
    {
        if (currentState!= null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
        for(int i = 0; i < patrolPoints.Length; i++)
        {
            Gizmos.color = wayPointGizmoColor;
            Gizmos.DrawWireSphere(patrolPoints[i], .25f);
        }
        for(int i = 0; i < path.Count; i++)
        {
            Gizmos.color = pathNodeColor;
            Gizmos.DrawWireSphere(path[i].position, .33f);
        }
    }
    

    public void TransitionToState(State nextState)
    {
        currentState = nextState;
    }

    public enum PatrolDirection
    {
        forwards,backwards
    }

    void RotateToMovement()
    {
        if (!vision.target)
        {
            Vector3 dir = rb2d.velocity;
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
