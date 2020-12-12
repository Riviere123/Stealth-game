using UnityEngine;

public class EnemyVisualCone : MonoBehaviour
{
    public float angle; //the field of view
    public int rays; //amount of rays to cast
    public float range; //the range of the vision
    public float timeToTrigger; //sets how long you must be in the cone before it triggers
    float endTime; //the time till trigger
    bool countDown = false; //check if we are already counting down
    bool aqcuireTarget = false;
    public Material material; //material the mesh uses
    public LayerMask rayCastObjects; //objects we want to hit
    public LayerMask objectThatObstruct; //this can only be one
    public LayerMask player; //this can only be one
    Vector3[] vertices; //array of vertices
    Vector2[] uv; //array of UVs
    int[] triangles; //array of triangles
    GameObject coneVisual; //the gameobject to create
    Mesh mesh; //mesh to create
    public GameObject target;

    private void Start()
    {
        mesh = new Mesh();
        vertices = new Vector3[rays + 1];
        uv = new Vector2[rays + 1];
        
        triangles = new int[(rays + 2) * 3];

        coneVisual = new GameObject("EnemyVisionCone", typeof(MeshFilter), typeof(MeshRenderer));
        coneVisual.GetComponent<MeshFilter>().mesh = mesh;
        coneVisual.GetComponent<MeshRenderer>().material = material;
        coneVisual.transform.localScale = new Vector3(1, 1, -1);

    }


    void Update()
    {

        var increment = angle / rays;
        var start = -(angle / 2);
        vertices[0] = transform.position;
        uv[0] = transform.position;

        bool seePlayer = false;

        for (int i = 1; i < rays; i++)
        {
            float tempAngle = start + (increment * i);
            var lDirection = DegreeToVector2(tempAngle + transform.eulerAngles.z);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, lDirection, range, rayCastObjects);
            Debug.DrawRay(transform.position, lDirection * range);
            if (hit)
            {
                if (hit.collider.gameObject.layer == layermask_to_layer(objectThatObstruct))
                {
                    vertices[i] = hit.point;
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
                if(hit.collider.gameObject.layer == layermask_to_layer(player))
                {
                    RaycastHit2D wallHit = Physics2D.Raycast(transform.position, lDirection, range, objectThatObstruct);
                    if (wallHit)
                    {
                        vertices[i] = wallHit.point;
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
                    else
                    {
                        vertices[i] = (transform.position + ((Vector3)lDirection * range));
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
                vertices[i] = (transform.position + ((Vector3)lDirection * range));
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
        }
        if (seePlayer)
        {

            if (countDown)
            {
                if (Time.time >= endTime)
                {
                    Debug.Log("Trigger");
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

    }
    static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    static Vector2 DegreeToVector2(float degree)
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
    static int layermask_to_layer(LayerMask layerMask)
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
}