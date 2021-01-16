using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMaster
{
    [SerializeField]
    HuD hud;
    [SerializeField]
    int gold;
    [SerializeField]
    List<GameObject> keys = new List<GameObject>();
    [SerializeField]
    List<GameObject> secretItems = new List<GameObject>();

    public PlayerMaster(HuD hud)
    {
        this.hud = hud;
    }
    /// <summary>
    /// Call this method when you want to add gold to the player.
    /// </summary>
    /// <param name="newGold">The ammount of gold to add.</param>
    public void AddGold(int newGold)
    {
        if(newGold < 0)
        {
            Debug.LogError($"cannot add a negative quantity of gold!");
        }
        else
        {
            gold += newGold;
            hud.DisplayGold();
        }
    }

    public int GetGold()
    {
        return gold;
    }

    public List<GameObject> GetKeys()
    {
        return keys;
    }

    public void AddKey(GameObject key)
    {
        keys.Add(key);
    }

    public void AddSecretItem(GameObject item)
    {
        secretItems.Add(item);
    }

    public List<GameObject> GetSecretItems()
    {
        return secretItems;
    }
}
