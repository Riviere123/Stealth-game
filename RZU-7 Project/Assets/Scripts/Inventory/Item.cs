using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public abstract void Use();
}
