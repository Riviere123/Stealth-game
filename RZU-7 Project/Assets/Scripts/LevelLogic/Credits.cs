using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    /// <summary>
    /// called from the main menu button loads the main menu scene.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
