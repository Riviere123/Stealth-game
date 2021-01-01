using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    LevelLogic levelLogic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            for(int i = 0; i < levelLogic.sDoors.Length; i++)
            {
                if(levelLogic.sDoors[i].Key == gameObject)
                {
                    levelLogic.sDoors[i].HasKey = true;
                    levelLogic.AddItem(GetComponent<SpriteRenderer>().sprite);
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
