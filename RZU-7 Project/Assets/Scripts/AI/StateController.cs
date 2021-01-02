using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public bool aiActive;
    public State currentState;
    
    public GameObject target;

    public PathFinding pathFinding;
    public Vector2[] patrolPoints;
    public Vector2 currentPatrolPoint;

    public List<Node> path;

    public Rigidbody2D rb2d;
    [SerializeField]
    Color wayPointGizmoColor;
    [SerializeField]
    Color pathNodeColor;

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
}
