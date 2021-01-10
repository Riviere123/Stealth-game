using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StateController : MonoBehaviour
{
    public References references;
    public List<StateMonoHelper> helpers;
    public bool aiActive;
    public State currentState;

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
        //for(int i = 0; i < patrolPoints.Length; i++)
        //{
        //    Gizmos.color = wayPointGizmoColor;
        //    Gizmos.DrawWireSphere(patrolPoints[i], .25f);
        //}
        //for(int i = 0; i < path.Count; i++)
        //{
        //    Gizmos.color = pathNodeColor;
        //    Gizmos.DrawWireSphere(path[i].position, .33f);
        //}
    }
    

    public void TransitionToState(State nextState)
    {
        currentState = nextState;
    }

    public T GetHelper<T>()
    {
        foreach (StateMonoHelper helper in helpers)
        {
            if (typeof(T).Equals(helper.GetType()))
            {
                return (T)(object)helper;
            }
        }

        Debug.LogError("Could not find StateMonoHelper object with name " + name);
        return default;
    }
}
