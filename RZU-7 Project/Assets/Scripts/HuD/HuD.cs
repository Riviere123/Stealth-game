﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




/// <summary>
/// Level Logic holds the logic behind the levels objectives, time, and score.
/// </summary>
/// <param name="playerMaster">Reference to the playermaster script</param>
/// <param name="levelTimer">Reference to the levelTimer script</param>
/// <param name="inventoryPanl">Reference to a UI gameobject that houses item images upon pickup.</param>
/// <param name="itemImage">The Prefab that we use to instantiate item images for the inventory panel.</param>
/// <param name="goldText">Reference to the text object that displays our current gold.</param>
/// <param name="timeText">Reference to the text object that displays our current time left.</param>

[System.Serializable]

public class HuD : MonoBehaviour
{
    [SerializeField]
    PlayerMaster playerMaster;
    [SerializeField]
    LevelTimer levelTimer;

    List<GameObject> keyImages = new List<GameObject>();

    [SerializeField]
    GameObject keyPanel;
    [SerializeField]
    GameObject itemImage;
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    TextMeshProUGUI timeText;

    private void Start()
    {
        playerMaster = new PlayerMaster(this);
        levelTimer = GetComponent<LevelTimer>();
    }

    /// <summary>
    /// Refreshes the gold display.
    /// </summary>
    public void DisplayGold()
    {
        goldText.text = playerMaster.GetGold().ToString();
    }

    /// <summary>
    /// Refreshes the time display.
    /// </summary>
    public void DisplayTime()
    {
        int minutes;
        int seconds;

        minutes = levelTimer.currentTime / 60;
        seconds = levelTimer.currentTime % 60;
        if (seconds < 10)
        {
            timeText.text = $"{minutes}:0{seconds}";
        }
        else
        {
            timeText.text = $"{minutes}:{seconds}";
        }
    }

    /// <summary>
    /// refreshes the keys display.
    /// </summary>
    public void DisplayKeys()
    {
        for(int i = 0; i < keyImages.Count; i++)
        {
            Destroy(keyImages[i].gameObject);
        }
        keyImages.Clear();

        List<GameObject> inventory = playerMaster.GetKeys();
        for(int i = 0; i < inventory.Count; i++)
        {
            GameObject item = Instantiate(itemImage, keyPanel.transform);
            keyImages.Add(item);
            try
            {
                item.GetComponent<Image>().sprite = inventory[i].GetComponent<SpriteRenderer>().sprite;
            }
            catch
            {
                Debug.LogError($"{item} does not have a sprite renderer");
            }
        }
    }
    /// <summary>
    /// gets the playerMaster script that is generated from this HuD Script.
    /// </summary>
    /// <returns>The playerMaster script reference.</returns>
    
    public PlayerMaster GetPlayerMaster()
    {
        return playerMaster;
    }

}

