using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach this to any object you wish to have an inventory.
/// </summary>
/// <param name="inventory"> This is the list of items representing our inventory.</param>
/// <param name="showingItems">The list of Gameobjects we use to show our items in the UI</param>
/// <param name="inventoryPanel">The UI Panel holding our inventory images.</param>
/// <param name="itemImage">The prefab item Image we create for the UI(The image is null and the image is assigned based on the items image)</param>

public class Inventory : MonoBehaviour
{
    [SerializeReference]
    List<Item> inventory = new List<Item>();
    List<GameObject> showingItems = new List<GameObject>();
    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject itemImage;


    [SerializeField]
    Sprite testItemSprite; ///THIS IS FOR TESTING PURPOSES ONLY.

    private void Update() ///THIS IS FOR TESTING PURPOSES ONLY!
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Key1 key = new Key1("Key","This is a Key.", testItemSprite);


            PickUp(key);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(inventory.Count > 0)
            {
                RemoveItem(inventory[0]);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].itemName == "Key")
                {
                    UseItem(inventory[i], true);
                    return;
                }
            }
        }
    }



    /// <summary>
    /// Updates the GUI For the inventory
    /// </summary>
    public void UpdateInventory()
    {
        ClearInventoryDisplay();

        for(int i = 0; i < inventory.Count; i++)
        {
            GameObject tempImage = Instantiate(itemImage, inventoryPanel.transform);
            tempImage.GetComponent<Image>().sprite = inventory[i].itemSprite;
            showingItems.Add(tempImage);
        }
    }

    /// <summary>
    /// Adds the designated item to the inventory.
    /// </summary>
    /// <param name="item">The item to add to the inventory.</param>
    public void PickUp(Item item)
    {
        inventory.Add(item);
        UpdateInventory();
    }


    /// <summary>
    /// Clears the inventory images.
    /// </summary>
    void ClearInventoryDisplay()
    {
        for(int i = 0; i < showingItems.Count; i++)
        {
            Destroy(showingItems[i]);
        }
        showingItems.Clear();
    }
    /// <summary>
    /// Removes the designated item from the inventory
    /// </summary>
    /// <param name="item">The item to remove from the Inventory.</param>
    public void RemoveItem(Item item)
    {
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            UpdateInventory();
        }
    }

    /// <summary>
    /// Use the desired item.
    /// </summary>
    /// <param name="item">The item to use.</param>
    /// <param name="destroy">True(Destroy Item) False(Keep Item) after use.</param>
    public void UseItem(Item item, bool destroy)
    {
        if (inventory.Contains(item))
        {
            item.Use();
        }
        if (destroy)
        {
            RemoveItem(item);
        }
    }
}
