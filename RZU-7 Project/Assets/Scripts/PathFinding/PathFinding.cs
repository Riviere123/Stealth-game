﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Grid grid;

    public List<Node> finalPath;

    public void FindPath(Vector2 a_startPos, Vector2 a_targetPos) //finds the closest path from start to target position
    {
        float startTime = Time.realtimeSinceStartup;
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

                if(neighborNode.isWall || closedList.Contains(neighborNode)) //this is where its broken
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
        Debug.Log("Found path in " + ((Time.realtimeSinceStartup - startTime) * 1000) + "ms");
    }

    void GetFinalPath(Node a_startingNode, Node a_endNode) //sets the final path to the correct 
    {
        /*finalPath = new List<Node>();*/
        finalPath.Clear();

        Node currentNode = a_endNode;

        while(currentNode != a_startingNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();

        /*grid.finalPath = finalPath;*/
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;

    }
}