using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds information that each level contains.
/// </summary>
/// <param name="displayName">The name that will be displayed on the button.</param>
/// <param name="sceneName">The name of the scene file. NOTE: This must be exact includes spaces and capitals and the scene must be in the Build Settings.</param>
/// <param name="badgesToUnlock">The ammount of badges until this level unlocks.</param>
/// <param name="unlocked">Stores if we have unlocked the level or not.</param>
/// <param name="speedBadge">Bool to tell if the badge has been unlocked.</param>
/// <param name="goldBadge">Bool to tell if the badge has been unlocked.</param>
/// <param name="secretBadge">Bool to tell if the badge has been unlocked.</param>
/// <param name="SceneName">Returns the sceneName variable.</param>
/// <param name="DisplayName">Returns the displayName variable.</param>
[System.Serializable]
public struct Level
{
    [SerializeField]
    string displayName;
    [SerializeField]
    string sceneName;

    [Range(0,100)]
    public int badgesToUnlock;
    public bool unlocked;

    public bool speedBadge;
    public bool goldBadge;
    public bool secretBadge;


    public string SceneName { get { return sceneName; } }
    public string DisplayName { get { return displayName; } }
}

/// <summary>
/// Holds a list of every Level and contains the logic for loading, unlocking, and collecting badges. This object will not be destroy when loading new scenes.
/// </summary>
/// <param name="currentLevelIndex">The Index for the level we are currently on. This is set when we use the load level method.</param>
/// <param name="totalBadges">The total badges unlocked.</param>
/// <param name="speedCount">The total speed badges unlocked.</param>
/// <param name="goldCount">The total gold badges unlocked.</param>
/// <param name="secretCount">The total secret badges unlocked.</param>
/// <param name="allLevels">An array of all our levels. Make level structs in the inspector.</param>
public class Levels : MonoBehaviour
{
    public int currentLevelIndex;
    public int totalBadges;
    public int speedCount;
    public int goldCount;
    public int secretCount;

    [SerializeField]
    public Level[] allLevels;


    /// <summary>
    /// We do not destroy this object when switching scenes and we calculate the badges on load.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        UpdateTotalBadges();
    }

    /// <summary>
    /// Iterates through every level and tallies how many badges have been unlocked for each badge type and total and assigned them, Then it runs UnlockLevelsBasedOnBadges().
    /// </summary>
    public void UpdateTotalBadges()
    {
        int spdcount = 0;
        int gldcount = 0;
        int scrtcount = 0;
        for(int i = 0; i < allLevels.Length; i++)
        {
            if (allLevels[i].speedBadge)
            {
                spdcount++;
            }
            if (allLevels[i].secretBadge)
            {
                scrtcount++;
            }
            if (allLevels[i].goldBadge)
            {
                gldcount++;
            }
        }
        speedCount = spdcount;
        goldCount = gldcount;
        secretCount = scrtcount;
        totalBadges = spdcount+scrtcount+gldcount;
        UnlockLevelsBasedOnBadges();
    }

    /// <summary>
    /// Checks all the levels to see if their badgesToUnlock is lower than or equal to the totalBadges Variable.
    /// </summary>
    public void UnlockLevelsBasedOnBadges()
    {
        for(int i = 0; i < allLevels.Length; i++)
        {
            if(allLevels[i].badgesToUnlock <= totalBadges)
            {
                allLevels[i].unlocked = true;
            }
        }
    }

    /// <summary>
    /// Loads the given level.
    /// </summary>
    /// <param name="lev">The level to load.</param>
    public void LoadLevel(Level lev)
    {
        var index = System.Array.IndexOf(allLevels, lev);
        if (lev.unlocked)
        {
            try
            {
                SceneManager.LoadScene(lev.SceneName);
                currentLevelIndex = index;
            }
            catch
            {
                Debug.LogError("The levels scene name is either incorrect or missing. Look at the proper name in 'File->BuildSettings->ScenesInBuild");
            }
        }
    }
    /// <summary>
    /// Loads the latest level unlocked.
    /// </summary>
    public void Continue()
    {
        for (int i = allLevels.Length - 1; i >= 0; i--)
        {
            Level lev = allLevels[i];
            if (lev.unlocked)
            {
                LoadLevel(lev);
                break;
            }
        }
    }
    /// <summary>
    /// Unlocks the secret badge for the provided level.
    /// </summary>
    /// <param name="lev">The level to unlock this badge for.</param>
    public void UnlockSecretBadge()
    {
        allLevels[currentLevelIndex].secretBadge = true;
        UpdateTotalBadges();
    }
    
    /// <summary>
    /// Unlocks the speed badge for the provided level.
    /// </summary>
    /// <param name="lev">The level to unlock this badge for.</param>
    public void UnlockSpeedBadge()
    {
        allLevels[currentLevelIndex].speedBadge = true;
        UpdateTotalBadges();
    }
    
    /// <summary>
    /// Unlocks the gold badge for the current level.
    /// </summary>
    public void UnlockGoldBadge()
    {
        allLevels[currentLevelIndex].goldBadge = true;
        UpdateTotalBadges();
    }
}
