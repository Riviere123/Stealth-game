using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererLimiter : MonoBehaviour
{
    /// <summary>
    /// ANYTHING YOU DO NOT WANT LIMITED OR DISABLED MARK WITH A TAG "SYSTEM" ON THE GAME OBJECT THIS WILL ONLY WORK ON OBJECTS WITH COLLIDERS!
    /// </summary>
    public Vector2 boxSize;
    public bool rendered;
    List<GameObject> containedObjects = new List<GameObject>();
    void Awake()
    {
        GetObjectsInside();
    }


    void Update()
    {
        if (rendered)
        {
            ToggleZoneOn();
        }
        else
        {
            ToggleZoneOff();
        }
    }
    void GetObjectsInside() //gets all the objects inside the zone when the object initializes
    {
        containedObjects.Clear();
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.GetComponent<RendererLimiter>())
            {
                continue;
            }
            if (hit.collider.gameObject.GetComponent<Camera>())
            {
                continue;
            }
            if (hit.collider.gameObject.GetComponent<Canvas>())
            {
                continue;
            }
            if(hit.collider.tag == "System")
            {
                continue;
            }
            containedObjects.Add(hit.collider.gameObject);
        }
    }
    public void ToggleZoneOn() //sets all the objects in the initial zone bounds to active
    {
        foreach(GameObject i in containedObjects)
        {
            i.SetActive(true);
        }
    }
    public void ToggleZoneOff() //sets all the objects in the initial zone bounds to NOT active
    {
        foreach(GameObject i in containedObjects)
        {
            i.SetActive(false);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

}
