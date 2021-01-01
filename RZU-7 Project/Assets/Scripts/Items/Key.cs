using UnityEngine;

/// <summary>
/// Key script that holds the logic for picking up keys.
/// </summary>
/// <param name="levelLogic">This should reference the levelLogic script on the scene that this key is in.</param>
public class Key : MonoBehaviour
{
    [SerializeField]
    LevelLogic levelLogic;
    /// <summary>
    /// checks if we collide with the player then cycles through the doors on the level and tells the struct that we have the key to the corosponding door then destroys this object.
    /// </summary>
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
