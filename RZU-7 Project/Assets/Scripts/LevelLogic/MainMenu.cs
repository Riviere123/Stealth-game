using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The logic behind the main menu also creates the levelsPrefab which stores our levels and keeps track of there unlocked state.
/// </summary>
/// <param name="levels">Reference to the levels class which gets created on the levelsprefab</param>
/// <param name="levelsPrefab">The prefab we load in. This houses all our assigned levels.</param>
public class MainMenu : MonoBehaviour
{
    Levels levels;
    [SerializeField]
    GameObject levelsPrefab;
    /// <summary>
    /// If the levelsPrefab doesn't already exist we make one.
    /// </summary>
    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("Levels"))
        {
            Instantiate(levelsPrefab);
        }
        if (GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>())
        {
            levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
        }
        else
        {
            Debug.Log("No Levels Gameobject was found.");
        }
    }
    /// <summary>
    /// Loads the level select scene.
    /// </summary>
    public void LevelSelectButton()
    {
        SceneManager.LoadScene("Level Select");
    }

    /// <summary>
    /// Loads the latest level unlocked.
    /// </summary>
    public void ContinueButton()
    {
        for (int i = levels.allLevels.Length-1; i >= 0; i--)
        {
            Level lev = levels.allLevels[i];
            if (lev.unlocked)
            {
                levels.LoadLevel(lev);
                break;
            }
        }
    }

    /// <summary>
    /// Loads the credits scene.
    /// </summary>
    public void CreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
}
