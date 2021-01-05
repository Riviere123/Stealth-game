using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{  
    public List<GameObject> Keys = new List<GameObject>();
    HuD hud;

    private void Awake()
    {
        hud = GameObject.FindGameObjectWithTag("HuD").GetComponent<HuD>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            if (!Keys.Contains(collision.gameObject))
            {
                Keys.Add(collision.gameObject);
                Destroy(collision.GetComponent<SpriteRenderer>());
                Destroy(collision.GetComponent<Collider2D>());
            }
        }
        if (collision.tag == "Valuable")
        {
            hud.AddGold(collision.GetComponent<Valuable>().value);
            Destroy(collision.GetComponent<SpriteRenderer>());
            Destroy(collision.GetComponent<Collider2D>());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (Keys.Contains(door.Key))
            {
                Debug.Log("I have the key!");
            }
            else
            {
                Debug.Log("I do not have the key!");
            }
        }
    }
}
