using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Houses data for each level
/// </summary>
/// <param name="displayName">The name that will be displayed on the button.</param>
/// <param name="sceneName">The name of the scene file. NOTE: This must be exact includes spaces and capitals and the scene must be in the Build Settings.</param>
/// <param name="unlocked">Stores if we have unlocked the level or not.</param>
[System.Serializable]
public struct Level
{
    [SerializeField]
    string displayName;
    [SerializeField]
    string sceneName;
    
    public bool unlocked;
    public bool speedBadge;
    public bool goldBadge;
    public bool secretBadge;


    public string SceneName { get { return sceneName; } }
    public string DisplayName { get { return displayName; } }
}

/// <summary>
/// The logic behind the level struct This class will be brought to EVERY scene past the main menu!
/// </summary>
/// <param name="allLevels">An array of all our levels. Make level structs in the inspector.</param>
public class Levels : MonoBehaviour
{
    [SerializeField]
    public Level[] allLevels;
    public int currentLevelIndex;

    /// <summary>
    /// We do not destroy this object when switching scenes.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Loads the level given the index of the allLevels array
    /// </summary>
    /// <param name="lev">The level to load.</param>
    public void LoadLevel(Level lev)
    {
        var index = System.Array.IndexOf(allLevels, lev);
        if (lev.unlocked)
        {
            SceneManager.LoadScene(lev.SceneName);
            currentLevelIndex = index;
        }
    }
    
    /// <summary>
    /// Unlocks the next level from current level index
    /// </summary>
    public void UnlockNextLevel()
    {
        var index = currentLevelIndex + 1;
        
        if(index > allLevels.Length - 1)
        {
            Debug.LogError("You're trying to unlock a level beyond the last level.");
        }
        else
        {
            allLevels[index].unlocked = true;
        }
    }
    
    /// <summary>
    /// Unlocks the secret badge for the provided level.
    /// </summary>
    /// <param name="lev">The level to unlock this badge for.</param>
    public void UnlockSecretBadge()
    {
        allLevels[currentLevelIndex].secretBadge = true;
    }
    
    /// <summary>
    /// Unlocks the speed badge for the provided level.
    /// </summary>
    /// <param name="lev">The level to unlock this badge for.</param>
    public void UnlockSpeedBadge()
    {
        allLevels[currentLevelIndex].speedBadge = true;
    }
    
    /// <summary>
    /// Unlocks the gold badge for the current level.
    /// </summary>
    public void UnlockGoldBadge()
    {
        allLevels[currentLevelIndex].goldBadge = true;
    }
}
