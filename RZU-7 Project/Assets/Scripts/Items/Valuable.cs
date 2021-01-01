using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : MonoBehaviour
{
    [SerializeField]
    LevelLogic levelLogic;
    [SerializeField]
    int value = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelLogic.AddGold(value);
            levelLogic.AddItem(GetComponent<SpriteRenderer>().sprite);
            Destroy(gameObject);
        }
    }
}
