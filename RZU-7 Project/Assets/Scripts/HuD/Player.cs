﻿using UnityEngine;

/// <summary>
/// This would be inside the players controller
/// </summary>
/// <param name"hud">Reference to the hud script</param>
/// <param name"playerMaster">Reference to the PlayerMasterScript</param>
public class Player: MonoBehaviour
{  
    HuD hud;
    PlayerMaster playerMaster;

    private void Start()
    {
        try
        {
            hud = GameObject.FindGameObjectWithTag("HuD").GetComponent<HuD>();
            playerMaster = hud.GetPlayerMaster();
        }
        catch
        {
            Debug.LogError($"Hud has no HuD Tag or is not in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Key")
        {
            if (!playerMaster.GetKeys().Contains(collision.gameObject))
            {
                GameObject key = collision.gameObject;
                playerMaster.AddKey(key);
                key.GetComponent<SpriteRenderer>().enabled = false;
                key.GetComponent<Collider2D>().enabled = false;
                hud.DisplayKeys();
            }
        }

        if (collision.tag == "Valuable")
        {
            playerMaster.AddGold(collision.GetComponent<Valuable>().value);
            Destroy(collision.GetComponent<SpriteRenderer>());
            Destroy(collision.GetComponent<Collider2D>());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (playerMaster.GetKeys().Contains(door.Key))
            {
                //This is where we would trigger door opening animation
                Debug.Log("I have the key!");
            }
            else
            {
                Debug.Log("I do not have the key!");
            }
        }
    }
}
