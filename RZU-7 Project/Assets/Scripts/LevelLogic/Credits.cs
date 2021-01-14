using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is where all our logic for the credits scene will go!
/// </summary>
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
