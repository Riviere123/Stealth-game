using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StateController : MonoBehaviour
{
    public bool aiActive;
    public State currentState;
    public AIStats stats;

    [HideInInspector]
    public PathFinding pathFinding;

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
        pathFinding = GetComponent<PathFinding>();
    }
    private void Update()
    {
        if (!aiActive)
        {
            return;
        }
        
        currentState.UpdateState(this);
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
    on
}
