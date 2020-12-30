using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to any gameobject and either asign it a grid or Enable findgridautomaticly to find the first grid in the heirarchy.
/// This script will allow you to use the grid provideds nodes to find a path.
/// </summary>
public class PathFinding : MonoBehaviour
{
    public Grid grid;
    public List<Node> finalPath;
    [SerializeField]
    bool findGridAutomaticly;

    private void Start()
    {
        if (findGridAutomaticly)
        {
            grid = GameObject.FindGameObjectWithTag("PathFindingGrid").GetComponent<Grid>();
        }
        
    }
    /// <summary>
    /// Finds the nearest path to the target position from the start position using the provided grid.
    /// </summary>
    /// <param name="a_startPos">The starting position of the path.</param>
    /// <param name="a_targetPos">The final position of the path.</param>
    public void FindPath(Vector2 a_startPos, Vector2 a_targetPos) //finds the closest path from start to target position
    {
        Node startNode = grid.NodeFromWorldPosition(a_startPos);
        Node targetNode = grid.NodeFromWorldPosition(a_targetPos);
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        openList.Add(startNode);

        while(openList.Count > 0)
        {
            Node currentNode = openList[0];

            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == targetNode)
            {
                GetFinalPath(startNode, targetNode);
            }

            foreach(Node neighborNode in grid.GetNeighboringNodes(currentNode))
            {
                if(neighborNode.isWall || closedList.Contains(neighborNode))
                {
                    continue;
                }

                int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);
                
                if(moveCost < neighborNode.gCost || !openList.Contains(neighborNode))
                {
                    neighborNode.gCost = moveCost;
                    neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                    neighborNode.parent = currentNode;

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets the finalpath list to the correct finalpath. This is used by the FindPath Script and does not need to be called outside this class.
    /// </summary>
    /// <param name="a_startingNode">The starting node.</param>
    /// <param name="a_endNode">The ending node.</param>
    void GetFinalPath(Node a_startingNode, Node a_endNode) //sets the final path to the correct 
    {
        finalPath.Clear();
        Node currentNode = a_endNode;

        while(currentNode != a_startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
    }

    /// <summary>
    /// Gets the rise over run distance from node a to node b to calculate node scores for pathfinding
    /// </summary>
    /// <param name="a_nodeA">First node to compare with.</param>
    /// <param name="a_nodeB">Second node to compare with.</param>
    /// <returns>Integer rise over run.</returns>
    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        return Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX) + Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);
    }
}
