﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Controls the EnemyVisual Cone. 
/// Attach this to an enemy AI and you will have full power over their individual visual field.
/// </summary>
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
    bool countDown; //check if we are already counting down
    bool aqcuireTarget; //if this is true and the player is in sight we will set target to the player

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

    }


    void Update()
    {


        var increment = angle / rays;
        var start = -(angle / 2);
        bool seePlayer = false;

        vertices[0] = transform.position + new Vector3(0, 0, 2);
        uv[0] = vertices[0];

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
                if (hit.collider.gameObject.layer == layermask_to_layer(player))
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


    /// <summary>
    /// Turns radian angles to a Vector2 direction
    /// </summary>
    /// <param name="radian">The radian angle to change.</param>
    /// <returns>Vector2 Direction</returns>
    Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    /// <summary>
    /// Turns degree angles to a Vector2 direction
    /// </summary>
    /// <param name="degree">The degree angle to change.</param>
    /// <returns>Vector2 Direction</returns>
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

    /// <summary>
    /// Gets the correct layer mask Int 
    /// </summary>
    /// <param name="layerMask">The layermask you want to get the int for.</param>
    /// <returns>Integer of layermask</returns>
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

    /// <summary>
    /// Sets the mesh positions to create the mesh.
    /// </summary>
    /// <param name="i">The place in the arrays</param>
    /// <param name="position">The Vector3 position to set the active point</param>
    void SetMeshStats(int i, Vector3 position)
    {
        vertices[i] = position + new Vector3(0, 0, 2);
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