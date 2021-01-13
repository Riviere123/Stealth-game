using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is run in TestScene 1 to test method calls for Levels script.
/// </summary>
public class TESTUnlockLevel : MonoBehaviour
{
    Levels levels;

    private void Start()
    {
        levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            levels.UnlockGoldBadge();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            levels.UnlockSecretBadge();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            levels.UnlockSpeedBadge();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
