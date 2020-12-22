using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class EnemyVisualCone : MonoBehaviour
{
    [SerializeField]
    float angle = 45; //the field of view
    [SerializeField]
    int rays = 20; //amount of rays to cast
    [SerializeField]
    float range = 5; //the range of the vision
    [SerializeField]
    float timeToTrigger = .5f; //sets how long you must be in the cone before it triggers

    float endTime; //the time till trigger
    bool countDown = false; //check if we are already counting down
    bool aqcuireTarget = false; //if this is true and the player is in sight we will set target to the player

    [SerializeField]
    Material material = null; //material the mesh uses
    [SerializeField]
    LayerMask rayCastObjects = 0; //objects we want to hit
    [SerializeField]
    LayerMask objectThatObstruct = 0; //this can only be one
    [SerializeField]
    LayerMask player = 0; //this can only be one

    Vector3[] vertices; //array of vertices
    Vector2[] uv; //array of UVs
    int[] triangles; //array of triangles

    GameObject coneVisual; //the gameobject to create
    Mesh mesh; //mesh to create
    MeshRenderer coneMeshRenderer;

    public GameObject target;

    GameObject playerObject;

    List<Vector3> positions = new List<Vector3>();
    List<Vector3> playerPositions = new List<Vector3>();
    private void Start()
    {
        mesh = new Mesh();


        vertices = new Vector3[rays + 1];
        uv = new Vector2[rays + 1];
        triangles = new int[(rays + 2) * 3];

        coneVisual = new GameObject("EnemyVisionCone", typeof(MeshFilter), typeof(MeshRenderer));
        coneMeshRenderer = coneVisual.GetComponent<MeshRenderer>();
        coneVisual.GetComponent<MeshFilter>().mesh = mesh;
        coneMeshRenderer.material = material;
        coneVisual.transform.localScale = new Vector3(1, 1, -1);
        coneVisual.layer = layermask_to_layer(LayerMask.GetMask("VisualCone"));

        playerObject = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        bool visible = false;
        for (int i = 0; i < positions.Count; i++)
        {
            Debug.DrawRay(positions[i], playerPositions[i] - positions[i], Color.red);
            if (CheckPlayerVisibility(positions[i], playerPositions[i]))
            {
                visible = true;
            }
        }

        if (visible)
        {
            coneMeshRenderer.enabled = true;
        }
        else
        {
            coneMeshRenderer.enabled = false;
        }
    }

    void Update()
    {
        playerPositions.Clear();
        positions.Clear();

        var increment = angle / rays;
        var start = -(angle / 2);
        bool seePlayer = false;

        vertices[0] = transform.position + new Vector3(0, 0, 2);
        uv[0] = vertices[0];

        //checking all 4 points top bottom left and right of the object for visibility ----see Late update
        positions.Add(new Vector3(transform.position.x + transform.localScale.x/2, transform.position.y));
        positions.Add(new Vector3(transform.position.x - transform.localScale.x/2, transform.position.y));
        positions.Add(new Vector3(transform.position.x , transform.position.y + transform.localScale.y / 2));
        positions.Add(new Vector3(transform.position.x , transform.position.y - transform.localScale.y / 2));

        playerPositions.Add(new Vector3(playerObject.transform.position.x + playerObject.transform.localScale.x / 2, playerObject.transform.position.y));
        playerPositions.Add(new Vector3(playerObject.transform.position.x - playerObject.transform.localScale.x / 2, playerObject.transform.position.y));
        playerPositions.Add(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y + playerObject.transform.localScale.y / 2));
        playerPositions.Add(new Vector3(playerObject.transform.position.x, playerObject.transform.position.y - playerObject.transform.localScale.y / 2));
       
        for (int i = 1; i < rays; i++)
        {
            float tempAngle = start + (increment * i);
            var lDirection = DegreeToVector2(tempAngle + transform.eulerAngles.z);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lDirection, range, rayCastObjects);

            if (hit)
            {
                if (hit.collider.gameObject.layer == layermask_to_layer(objectThatObstruct))
                {
                    SetMeshStats(i, hit.point);
                }
                if(hit.collider.gameObject.layer == layermask_to_layer(player))
                {
                    RaycastHit2D wallHit = Physics2D.Raycast(transform.position, lDirection, range, objectThatObstruct);
                    if (wallHit)
                    {
                        SetMeshStats(i, wallHit.point);
                    }
                    else
                    {
                        SetMeshStats(i, (transform.position + ((Vector3)lDirection * range)));
                    }
                    
                    if (aqcuireTarget) //This is the trigger event
                    {
                        target = hit.collider.gameObject;
                        aqcuireTarget = false;
                    }
                    seePlayer = true;
                }
            }
            else
            {
                SetMeshStats(i, (transform.position + ((Vector3)lDirection * range)));
            }  
        }

        if (seePlayer)
        {
            if (countDown)
            {
                if (Time.time >= endTime)
                {
                     //this is the trigger after the end time
                    aqcuireTarget = true;
                    countDown = false;
                }
            }
            else
            {
                countDown = true;
                endTime = Time.time + timeToTrigger;
            }
        }
        else
        {
            target = null;
            countDown = false;
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }
    
    Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    void OnDrawGizmos()
    {
        var increment = angle / rays;
        var start = -(angle / 2);
        for (int i = 1; i < rays; i++)
        {
            float tempAngle = start + (increment * i);
            var lDirection = DegreeToVector2(tempAngle + transform.eulerAngles.z);

            Gizmos.DrawRay(transform.position, lDirection * range);
        }
    }
    int layermask_to_layer(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }

    void SetMeshStats(int i, Vector3 position)
    {
        vertices[i] = position + new Vector3(0,0,2);
        uv[i] = vertices[i];
        triangles[i * 3] = 0;
        triangles[i * 3 + 1] = i;
        if (i + 1 < rays)
        {
            triangles[i * 3 + 2] = i + 1;
        }
        else
        {
            triangles[i * 3 + 2] = 0;
        }
    } 

    bool CheckPlayerVisibility(Vector3 position, Vector3 targetposition)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, targetposition - position, Vector2.Distance(position,targetposition),rayCastObjects);

        if (hit)
        {
            if (hit.collider.gameObject == playerObject)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        return false;      
    }
}