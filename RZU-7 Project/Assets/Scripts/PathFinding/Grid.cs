using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    LayerMask wallMask; //what object masks we are looking for collision with
    [SerializeField]
    Vector2 gridWorldSize = new Vector2(30,30);
    [SerializeField]
    Vector2 startPosition = new Vector2(0,0);
    [SerializeField]
    float nodeRadius = .5f; //size of the collision circle to check
    [SerializeField]
    float distance = 0; //visual distance between gizmos

    Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    Color white = new Color(1, 1, 1, .2f);
    Color yellow = new Color(.5f, .25f, .1f, .2f);

    [HideInInspector]
    public PathFinding pathFinding;


    private void Awake()
    {
       pathFinding = new PathFinding(this);
    }
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
        myX = Mathf.Clamp(myX, 0, gridSizeX - 1);
        myY = Mathf.Clamp(myY, 0, gridSizeY - 1);
        Node node = grid[(int)myX, (int)myY];
        if (node.isWall)
        {
            if((int)myX < gridSizeX)
            {
                return grid[(int)myX + 1, (int)myY];
            }
            else
            {
                return grid[(int)myX, (int)myY];
            }
        }
        else
        {
            return grid[(int)myX, (int)myY];
        }
    }

    public List<Node> GetNeighboringNodes(Node a_Node) //gets all the neighboring nodes
    {
        
        List<Node> neighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //right side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if (XandYChecker(xCheck,yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        //Left side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }
        //Up side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }
        //Down side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        //DownRight side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY - 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        //DownLeft side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY - 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        //upRight side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY + 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        //upLeft side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY + 1;
        if (XandYChecker(xCheck, yCheck))
        {
            neighboringNodes.Add(grid[xCheck, yCheck]);
        }

        return neighboringNodes;
    }

    bool XandYChecker(int x, int y)
    {
        if (x >= 0 && x < gridSizeX)
        {
            if (y >= 0 && y < gridSizeY)
            {
                return true;
            }
        }

        return false;
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
                    Gizmos.color = white;
                }
                else
                {
                    Gizmos.color = yellow;
                }
                Gizmos.DrawCube(node.position, Vector2.one * (nodeDiameter - distance)); //draw the node at the position of the node.
            }
        }
    }
}
