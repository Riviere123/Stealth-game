using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the logic for the Level Select Scene. This handles the creation of the buttons and allows them to load scenes if unlocked.
/// </summary>
/// <param name="levels">Reference to the levels class.</param>
/// <param name="button">Reference to the button prefab.</param>
/// <param name="levelsPanel">Reference to the levelspanel UI Panel gameobject in the scene. (This houses the buttons and positions them)</param>
/// <param name="lockedColor">The color the locked buttons will be tinted to.</param>
public class LevelSelect : MonoBehaviour
{
    Levels levels;
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject levelsPanel;
    [SerializeField]
    Color lockedColor;
    [SerializeField]
    Color lockedBadge;
    private void Awake()
    {
        try
        {
            if (GameObject.FindGameObjectWithTag("Levels"))
            {
                if (GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>())
                {
                    levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
                }
            }
        }
        catch
        {
            Debug.LogError("Error with either finding the Tag Levels or object getting the component named Levels from the gameobject tagged Levels");
        }
        
    }

    /// <summary>
    /// Cycles threw all the levels and checks if they are unlocked. If it's not unlocked it tints it. if it is unlocked we assign the buttons function when clicked to load the corresponding level.
    /// </summary>
    private void Start()
    {
        for(int i = 0; i < levels.allLevels.Length; i++)
        {
            Level current = levels.allLevels[i];
            GameObject temp = Instantiate(button, levelsPanel.transform);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = current.DisplayName;
            if (!current.unlocked)
            {
                temp.GetComponent<Image>().color = lockedColor;
            }
            else
            {
                temp.GetComponent<Button>().onClick.AddListener(delegate { levels.LoadLevel(current); });
            }
            if (!levels.allLevels[i].goldBadge)
            {
                temp.transform.GetChild(0).GetComponent<Image>().color = lockedBadge;
            }
            if (!levels.allLevels[i].secretBadge)
            {
                temp.transform.GetChild(1).GetComponent<Image>().color = lockedBadge;
            }
            if (!levels.allLevels[i].speedBadge)
            {
                temp.transform.GetChild(2).GetComponent<Image>().color = lockedBadge;
            }
        }
    }

    /// <summary>
    /// Loads the main menu.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
