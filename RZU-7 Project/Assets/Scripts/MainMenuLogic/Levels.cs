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
    [SerializeField]
    public bool unlocked;


    public string SceneName { get { return sceneName; } }
    public string DisplayName { get { return displayName; } }
}
/// <summary>
/// The logic behind the level struct This class will be brought to EVERY scene past the main menu!
/// </summary>
/// <param name="allLevels">An array of all our levels. Make level structs in the inspector.</param>
public class Levels : MonoBehaviour
{
    public Level[] allLevels;
    

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
    /// <param name="i">The allLevels index to load.</param>
    public void LoadLevel(int i)
    {
        if (allLevels[i].unlocked)
        {
            SceneManager.LoadScene(allLevels[i].SceneName);
        }
    }
    /// <summary>
    /// Unlocks the level given the index of the allLevels array.
    /// </summary>
    /// <param name="i">The allLevels index to unlock.</param>
    public void UnlockLevel(int i)
    {
        if (!allLevels[i].unlocked)
        {
            allLevels[i].unlocked = true;
        }
    }
}
