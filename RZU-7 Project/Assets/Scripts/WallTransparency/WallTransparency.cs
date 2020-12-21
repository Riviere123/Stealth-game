using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTransparency : MonoBehaviour
{
    Tilemap tm;
    Color transparent = new Color(1, 1, 1, .75f);
    Color solid = new Color(1, 1, 1, 1);
    List<GameObject> activeObjects = new List<GameObject>();
    private void Start()
    {
        tm = GetComponent<Tilemap>();
    }
    private void Update()
    {
        if(activeObjects.Count > 0)
        {
            tm.color = transparent;
        }
        else
        {
            tm.color = solid;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (!activeObjects.Contains(collision.gameObject))
            {
                activeObjects.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (activeObjects.Contains(collision.gameObject))
            {
                activeObjects.Remove(collision.gameObject);
            }
        }
    }
}
