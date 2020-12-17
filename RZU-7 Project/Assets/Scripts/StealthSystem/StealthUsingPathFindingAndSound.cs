using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthUsingPathFindingAndSound : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 15;
    [SerializeField]
    RotationDirection rotationDirection = RotationDirection.None;
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float distanceFromTargetToStop = .75f;
    [SerializeField]
    float recalculateDistance = 1;
    public GameObject target;
    PathFinding pf;
    EnemyVisualCone evc;
    List<Node> pathNodes = new List<Node>();
    [SerializeField]
    State state;
    Rigidbody2D rb2d;

    private void Start()
    {
        evc = GetComponent<EnemyVisualCone>();
        pf = GetComponent<PathFinding>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {

        switch (state)
        {
            case State.LookCircle:
                
                if (!target)
                {
                    if(rotationDirection == RotationDirection.None)
                    {
                        rotationDirection = (RotationDirection)Random.Range(0, 2);
                    }
                    else if(rotationDirection == RotationDirection.Left)
                    {
                        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
                    }
                    else if (rotationDirection == RotationDirection.Right)
                    {
                        transform.Rotate(0, 0, Time.deltaTime * -rotationSpeed);
                    } 
                }
                if (evc.target != null)
                {
                    target = evc.target;
                    state = State.Chase;
                }

                break;


            case State.Chase:

                rotationDirection = RotationDirection.None;
                target = evc.target;

                if (target)
                {
                    var dir = target.transform.position - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * (rotationSpeed / 2));

                    if(pathNodes.Count <= 0 || Vector2.Distance(target.transform.position, pathNodes[pathNodes.Count - 1].position) >= recalculateDistance)
                    {
                        pf.FindPath(transform.position, target.transform.position);
                        pathNodes = pf.finalPath;
                    }
                }

                if(!target && pathNodes.Count > 0)
                {
                    var dir = pathNodes[0].position - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * (rotationSpeed / 2));
                }

                if (pathNodes.Count > 0)
                {
                    if (Vector2.Distance(pathNodes[0].position, transform.position) < distanceFromTargetToStop)//if we are close enough to the node remove it and move onto the next node
                    {
                        pathNodes.RemoveAt(0);
                    }
                }

                if (!target && pathNodes.Count <= 0)
                {
                    state = State.LookCircle;
                }

                break;
        }
    }

    private void FixedUpdate()
    {
        if (pathNodes.Count != 0)//if there are path nodes
        {
            rb2d.AddForce((pathNodes[0].position - transform.position).normalized * moveSpeed); //adds an arbitrary force * moveSpeed in the direction of the next node
        }
    }

    public enum RotationDirection
    {
        Right, Left, None
    }
    public enum State
    {
        Chase, LookCircle
    }
}
