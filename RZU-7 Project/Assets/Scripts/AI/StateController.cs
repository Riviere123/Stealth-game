using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class StateController : MonoBehaviour
{
    [SerializeField]
    public List<MonoHelperSet> helpers;
    public bool aiActive;
    public State currentState;

    private void Awake()
    {
        SetHelperNames();
    }

    private void Update()
    {
        if (!aiActive)
        {
            return;
        }
        
        currentState.UpdateState(this);
        //RotateToMovement();
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

    //void RotateToMovement()
    //{
    //    if (!vision.target)
    //    {
    //        Vector3 dir = rb2d.velocity;
    //        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    }
    //    else
    //    {
    //        Vector3 dir = vision.target.transform.position - transform.position;
    //        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    //        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //    }
        
    //}

    public StateMonoHelper GetHelper(string name)
    {
        foreach (MonoHelperSet helperSet in helpers)
        {
            if (helperSet.GetName().Equals(name))
            {
                return helperSet.GetHelper();
            }
        }

        Debug.LogError("Could not find StateMonoHelper object with name " + name);
        return null;
    }

    public List<string> GetHelperNames()
    {
        List<string> names = new List<string>();

        foreach (MonoHelperSet helperSet in helpers)
        {
            names.Add(helperSet.GetName());
        }

        return names;
    }

    private void SetHelperNames()
    {
        foreach (MonoHelperSet helperSet in helpers)
        {
            helperSet.GetHelper().SetName(helperSet.GetName());
        }
    }
}
