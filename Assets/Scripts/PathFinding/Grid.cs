using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public LayerMask wallMask; //what object masks we are looking for collision with
    public Vector2 gridWorldSize;
    public Vector2 startPosition;
    public float nodeRadius; //size of the collision circle to check
    public float distance; //visual distance between gizmos
    

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();

    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeft = (startPosition - (Vector2.right * ((gridWorldSize.x / 2)))  - (Vector2.up * ((gridWorldSize.y / 2))) + new Vector2(nodeRadius,nodeRadius));
    
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = bottomLeft + Vector2.right * (x * nodeDiameter) + Vector2.up * (y * nodeDiameter);

                /*Debug.Log($"worldpoint{worldPoint} for {x}/{y}");*/
                bool wall = false;

                RaycastHit2D hit = Physics2D.CircleCast(worldPoint, nodeRadius, Vector2.zero);
                if(hit)
                {
                    if(hit.collider.gameObject.layer == Mathf.Log(wallMask.value, 2))
                    {
                        wall = true;
                    }                    
                }

                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector2 a_worldPosition) 
    {
        Vector2 bottomLeft = (startPosition - (Vector2.right * ((gridWorldSize.x / 2))) - (Vector2.up * ((gridWorldSize.y / 2))) + new Vector2(nodeRadius, nodeRadius));

        float myX = ((a_worldPosition.x - bottomLeft.x) / nodeDiameter); //gets the grid position x relative to the world position
        float myY = ((a_worldPosition.y - bottomLeft.y) / nodeDiameter); //gets the grid position y relative to the world position
        if (myX > gridSizeX-1)
        {
            myX = gridSizeX-1;
        }
        if(myX < 0)
        {
            myX = 0;
        }
        if(myY > gridSizeY-1)
        {
            myY = gridSizeY-1;
        }
        if(myY < 0)
        {
            myY = 0;
        }
        
        return grid[(int)myX, (int)myY];
        
    }

    public List<Node> GetNeighboringNodes(Node a_Node) //gets all the neighboring nodes
    {
        
        List<Node> neighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //right side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Left side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Up side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Down side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        
        //DownRight side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //DownLeft side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //upRight side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        //upLeft side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        return neighboringNodes;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(startPosition, new Vector2(gridWorldSize.x, gridWorldSize.y)); //draw a wire cube with the given dimentions

        if(grid != null) //if the grid is not empty
        {
            foreach(Node node in grid) //loop through every node in the grid
            {
                if (node.isWall)//if the current node is a wall node
                {
                    Gizmos.color = new Color(1,1,1,.5f); //set the color of the node
                }
                else
                {
                    Gizmos.color = new Color(.5f,.25f,.1f,.5f); //set the color of the node
                }

                if(finalPath != null) //if the final path is not empty
                {
                    if (finalPath.Contains(node)) //if the current node is in the final path
                    {
                        Gizmos.color = Color.red; //set the color of the node
                    }   
                }
                Gizmos.DrawCube(node.position, Vector2.one * (nodeDiameter - distance)); //draw the node at the position of the node.
            }
        }
    }
}
