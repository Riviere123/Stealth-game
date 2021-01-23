using System.Collections.Generic;
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
    GameMaster gameMaster;
    PlayerMaster playerMaster;
    LevelTimer levelTimer;

    List<GameObject> keyImages = new List<GameObject>();
    List<GameObject> secretImages = new List<GameObject>();

    [SerializeField]
    GameObject keyPanel;
    [SerializeField]
    GameObject secretPanel;
    [SerializeField]
    GameObject itemImage;
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    TextMeshProUGUI timeText;

    [SerializeField]
    Color unlockedBadgeColor;

    [SerializeField]
    GameObject completePanel;
    [SerializeField]
    Image completeSpeedBadge;
    [SerializeField]
    Image completeGoldBadge;
    [SerializeField]
    Image completeSecretBage;
    [SerializeField]
    TextMeshProUGUI completeSpeedText;
    [SerializeField]
    TextMeshProUGUI completeSecretText;
    [SerializeField]
    TextMeshProUGUI completeGoldText;

    [SerializeField]
    Button homeButton;
    private void Start()
    {
        playerMaster = gameMaster.GetPlayerMaster();
        levelTimer = gameMaster.GetLevelTimer();
        DisplayTime();
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
        timeText.text = ConvertToTime(levelTimer.currentTime);
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

    public void CompletePanelEnable()
    {
        Levels levs = gameMaster.GetLevels();
        completePanel.SetActive(true);
        completeGoldText.text = $"{playerMaster.GetGold().ToString()}/{levs.allLevels[levs.currentLevelIndex].goldToUnlock}";
        completeSecretText.text = $"{playerMaster.GetSecretItems().Count.ToString()}/{levs.allLevels[levs.currentLevelIndex].secretsToUnlock}";
        completeSpeedText.text = $"{ConvertToTime(levelTimer.GetStartTime() - levelTimer.currentTime)}/{ConvertToTime(levs.allLevels[levs.currentLevelIndex].speedToUnlock)}";
        if(playerMaster.GetGold() >= levs.allLevels[levs.currentLevelIndex].goldToUnlock)
        {
            completeGoldBadge.color = unlockedBadgeColor;
        }

        if(playerMaster.GetSecretItems().Count >= levs.allLevels[levs.currentLevelIndex].secretsToUnlock)
        {
            completeSecretBage.color = unlockedBadgeColor;
        }

        if((levelTimer.GetStartTime() - levelTimer.currentTime) <= levs.allLevels[levs.currentLevelIndex].speedToUnlock)
        {
            completeSpeedBadge.color = unlockedBadgeColor;
        }
        homeButton.onClick.AddListener(delegate { levs.MainMenu(); });
    }
    public string ConvertToTime(int time)
    {
        int minutes;
        int seconds;

        minutes = time / 60;
        seconds = time % 60;
        if (seconds < 10)
        {
            return ($"{minutes}:0{seconds}");
        }
        else
        {
            return ($"{minutes}:{seconds}");
        }
    }
    public void DisplaySecretItems()
    {
        for (int i = 0; i < secretImages.Count; i++)
        {
            Destroy(secretImages[i].gameObject);
        }
        secretImages.Clear();

        List<GameObject> inventory = playerMaster.GetSecretItems();
        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject item = Instantiate(itemImage, secretPanel.transform);
            secretImages.Add(item);
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
}

