using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public bool aiActive;
    public GameObject target;

    public PathFinding pathFinding;
    public Vector2[] patrolPoints;

    public List<Node> Path;

    public Rigidbody2D rb2d;
    [SerializeField]
    Color wayPointGizmoColor;

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
    }

    public void TransitionToState(State nextState)
    {
        currentState = nextState;
    }
}
