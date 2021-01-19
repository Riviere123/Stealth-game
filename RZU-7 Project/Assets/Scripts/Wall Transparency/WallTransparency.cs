using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTransparency : MonoBehaviour
{
    Color transparent = new Color(1, 1, 1, .5f);
    Color solid = new Color(1, 1, 1, 1);
    Tilemap tilemap;
    private void Awake()
    {
       tilemap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            tilemap.color = transparent;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            tilemap.color = solid;
        }
    }
}
