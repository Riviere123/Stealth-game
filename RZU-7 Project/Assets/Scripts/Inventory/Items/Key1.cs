using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1 : Item
{
    public Key1(string ItemName, string ItemDescription, Sprite ItemSprite)
    {
        itemName = ItemName;
        itemDescription = ItemDescription;
        itemSprite = ItemSprite;
    }
    public override void Use()
    {
        Debug.Log("used key");
    }
}
