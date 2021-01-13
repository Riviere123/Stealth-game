using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TESTUnlockLevel : MonoBehaviour
{
    Levels levels;

    private void Start()
    {
        levels = GameObject.FindGameObjectWithTag("Levels").GetComponent<Levels>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            levels.UnlockNextLevel();
        }
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
