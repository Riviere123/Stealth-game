using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct KeyToDoor
{
    public GameObject Key;
    public GameObject Door;
    public bool HasKey;
}

public class LevelLogic : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject itemImage;
    public KeyToDoor[] sDoors;

    private void OnGUI()
    {
        for(int i = 0; i < sDoors.Length; i++)
        {
            if (sDoors[i].HasKey)
            {
                sDoors[i].Door.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                sDoors[i].Door.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    public void AddItem(Sprite sprite)
    {
        GameObject tempObject = Instantiate(itemImage,inventoryPanel.transform);
        tempObject.GetComponent<Image>().sprite = sprite;
    }
}

