using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFindingReference : MonoBehaviour
{
    [SerializeField]
    PathFinding pathFinding;// the pathfinding script attached to this object
    public float moveSpeed = 1; //the enemies move speed
    public GameObject target; //the target the enemy will pathfind to
    public State state; //simple state machine to tell if the enemy is hunting or idle
    
    Rigidbody2D rb2d; //this objects rigidbody

    public List<Node> pathNodes; //this is the path of nodes that the enemy will follow

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //gets the rigidbody attached to this object
        pathFinding = GetComponent<PathFinding>(); //gets the pathfinding script attached to this object
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) //start of state machine
        {
            case State.Idle: //if the state is idle
                pathNodes.Clear(); //clear the nodes
                break;

            case State.Hunt: //if the state is hunt
                if (target && pathNodes.Count == 0 && Vector2.Distance(target.transform.position, transform.position) > 3)//if we have a target and no nodes
                {
                    pathFinding.FindPath(transform.position, target.transform.position); //find the closest path to the target
                    pathNodes = pathFinding.finalPath; //sets the pathnodes list to to correct path to reach the target
                }
                if(pathNodes.Count > 0)//if there are path nodes
                {
                    if(Vector2.Distance(pathNodes[0].position,transform.position) < .75)//if we are close enough to the node remove it and move onto the next node
                    {
                        pathNodes.RemoveAt(0);
                    }
                }
                break;
        }
    }
    
    private void FixedUpdate()
    {
        if(pathNodes.Count != 0)//if there are path nodes
        {
            rb2d.AddForce((pathNodes[0].position - transform.position).normalized * moveSpeed); //adds an arbitrary force * moveSpeed in the direction of the next node
        }
    }
    public enum State //simple enemy states
    {
        Idle, Hunt
    }
}
