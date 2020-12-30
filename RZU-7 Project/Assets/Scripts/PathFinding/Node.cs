using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node 
{
    public int gridX; //x position in the node array
    public int gridY; //y position in the node array

    public bool isWall; //tells the program if this node is being obstructed.
    public Vector3 position; //the world position of the node

    public Node parent; //for the algorithm, will store what node it previously came from to trace back shortest path

    public int gCost;//the cost of moving to the next square.
    public int hCost;//the distance to the goal from this node.
    public int fCost { get { return gCost + hCost; } } //adds both costs together

    public Node(bool a_isWall, Vector3 a_Pos, int a_gridX, int a_gridY)
    {
        isWall = a_isWall;
        position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }
}
