using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    PlayerMaster playerMaster;
    [SerializeField]
    LevelTimer levelTimer;
    [SerializeField]
    HuD hud;
    Levels levels;
    [SerializeField]
    GameObject TestLevels;
    private void Awake()
    {
        playerMaster = new PlayerMaster(hud);
        try
        {
            levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
        }
        catch
        {
            
            GameObject temp = Instantiate(TestLevels);
            levels = temp.GetComponent<Levels>();
        }
    }

    public PlayerMaster GetPlayerMaster()
    {
        return playerMaster;
    }
    public LevelTimer GetLevelTimer()
    {
        return levelTimer;
    }
    public HuD GetHud()
    {
        return hud;
    }
    public Levels GetLevels()
    {
        return levels;
    }
    public void CompleteLevel()
    {
        hud.CompletePanelEnable();
        Debug.Log("Completed Level");
        if((levelTimer.GetStartTime() - levelTimer.currentTime) <= levels.allLevels[levels.currentLevelIndex].speedToUnlock)
        {
            levels.UnlockSpeedBadge();
        }
        if(playerMaster.GetGold() >= levels.allLevels[levels.currentLevelIndex].goldToUnlock)
        {
            levels.UnlockGoldBadge();
        }
        if(playerMaster.GetSecretItems().Count >= levels.allLevels[levels.currentLevelIndex].secretsToUnlock)
        {
            levels.UnlockSecretBadge();
        }
    }
}
