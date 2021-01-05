using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


[System.Serializable]

/// <summary>
/// Level Logic holds the logic behind the levels objectives, time, and score.
/// </summary>
/// <param name="inventoryPanl">Reference to a UI gameobject that houses item images upon pickup.</param>
/// <param name="itemImage">The Prefab that we use to instantiate item images for the inventory panel.</param>
/// <param name="goldText">Reference to the text object that displays our current gold.</param>
/// <param name="timeText">Reference to the text object that displays our current time left.</param>
/// <param name="nextLevel">This is the string name of the next level scene.</param>
/// <param name="gold">The players score value.</param>
/// <param name="levelTime">The ammount of time the player has on the level in seconds.</param>
/// <param name="currentTime">The current time remaining in the level.</param>
/// <param name="Pause">If true pause time.</param>
public class HuD : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    GameObject itemImage;
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    string nextLevel;
    [SerializeField]
    int gold;
    [SerializeField]
    int levelTime;
    [SerializeField]
    int currentTime;
    [SerializeField]
    bool pause;

    private void Awake()
    {
        currentTime = levelTime;
    }
    private void Start()
    {
        StartCoroutine(CountDown());
    }

    /// <summary>
    /// Call this method when you want to add an item image to the players inventory.
    /// </summary>
    /// <param name="sprite">The image to add to the inventory panel</param>
    public void AddItem(Sprite sprite)
    {
        GameObject tempObject = Instantiate(itemImage,inventoryPanel.transform);
        tempObject.GetComponent<Image>().sprite = sprite;
    }
    
    /// <summary>
    /// Call this method when you want to add gold to the player.
    /// </summary>
    /// <param name="newGold">The ammount of gold to add.</param>
    public void AddGold(int newGold)
    {
        gold += newGold;
        goldText.text = gold.ToString();
    }

    /// <summary>
    /// Game Over method.
    /// </summary>
    void GameOver()
    {
        Debug.Log("Game Over!");
    }
    /// <summary>
    /// Loads the scene with the provided name. NOTE: Make sure the level is in the build scenes list under 'File->Build Settings->add open scene to add the current scene opened.
    /// </summary>
    /// <param name="SceneName">The name of the scene to load. This scene must be in the build scenes.</param>
    void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    /// <summary>
    /// pauses the decrement of time.
    /// </summary>
    void PauseGame()
    {
        pause = true;
    }
    /// <summary>
    /// unpauses the decrement of time.
    /// </summary>
    void UnPauseGame()
    {
        pause = false;
    }

    /// <summary>
    /// This counts down the timer and displays the correct time in the time text gameobject.
    /// </summary>
    /// <returns>Wait for 1 seconds</returns>
    IEnumerator CountDown()
    {
        if (currentTime <= 0)
        {
            GameOver();
        }

        int minutes;
        int seconds;

        minutes = currentTime / 60;
        seconds = currentTime % 60;
        if(seconds < 10)
        {
            timeText.text = $"{minutes}:0{seconds}";
        }
        else
        {
            timeText.text = $"{minutes}:{seconds}";
        }


        if (currentTime != 0)
        {
            yield return new WaitForSeconds(1);
            if (!pause)
            {
                currentTime -= 1;
            }
            StartCoroutine(CountDown());
        }
    }
}

