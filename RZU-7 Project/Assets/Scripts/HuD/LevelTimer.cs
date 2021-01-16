using System.Collections;
using UnityEngine;

/// <summary>
/// Controlls the timer for the level
/// </summary>
/// <param name="hud">Reference to the HUD Script (This script should be on the same game object.</param>
/// <param name="pause">Pauses the time.</param>
/// <param name="levelTime">The starting time for the level.</param>
/// <param name="currentTime">The remaining tim of the level.</param>
public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    HuD hud;
    [SerializeField]
    bool pause;
    [SerializeField]
    int levelTime;
    public int currentTime;

    private void Start()
    {
        currentTime = levelTime;
        StartCoroutine(CountDown());
    }

    /// <summary>
    /// pauses the decrement of time.
    /// </summary>
    void Pause()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    /// <summary>
    /// This counts down the timer and displays the correct time in the time text gameobject.
    /// </summary>
    /// <returns>Wait for 1 seconds</returns>
    IEnumerator CountDown()
    {
        if (currentTime <= 0)
        {
            //GameOver call goes here
            Debug.Log("Times Up!");
        }

        if (currentTime > 0)
        {
            yield return new WaitForSeconds(1);
            currentTime -= 1;
            Mathf.Clamp(currentTime, 0, Mathf.Infinity);
            hud.DisplayTime();
            StartCoroutine(CountDown());
        }
    }

    public int GetStartTime()
    {
        return levelTime;
    }
}
