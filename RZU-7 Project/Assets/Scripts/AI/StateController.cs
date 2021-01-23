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
    public Vector2 heardPosition;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sound")
        {
            if (heardPosition == Vector2.zero && currentState.ToString() != "Investigate State (State)")
            {
                heardPosition = collision.transform.position;
            }
            if(GetHelper<MovementHelper>().path.Count > 0)
            {
                MovementHelper mh = GetHelper<MovementHelper>();
                if (Vector2.Distance(mh.path[mh.path.Count - 1].position, transform.position) > Vector2.Distance(transform.position, collision.transform.position) + 5f)
                {
                    heardPosition = collision.transform.position;
                }
            }
        }
    }
}
