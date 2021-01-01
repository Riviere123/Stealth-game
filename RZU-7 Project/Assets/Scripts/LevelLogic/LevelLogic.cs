using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField]
    TextMeshProUGUI goldText;
    [SerializeField]
    GameObject timeImage;
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
    public KeyToDoor[] sDoors;

    private void Awake()
    {
        currentTime = levelTime;
    }
    private void Start()
    {
        StartCoroutine(CountDown());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadLevel(nextLevel);
        }
    }
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
    
    public void AddGold(int newGold)
    {
        gold += newGold;
        goldText.text = gold.ToString();
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
    }
    void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

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
            currentTime -= 1;
            StartCoroutine(CountDown());
        }
    }
}

