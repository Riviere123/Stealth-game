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
    private void Awake()
    {
        playerMaster = new PlayerMaster(hud);
        levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
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

    public void CompleteLevel()
    {
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
