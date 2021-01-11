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
    HuD hud;
    [SerializeField]
    bool pause;
    [SerializeField]
    int levelTime;
    public int currentTime;

    private void Start()
    {
        hud = GetComponent<HuD>();
        currentTime = levelTime;
        StartCoroutine(CountDown());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Pause();
        }
    }
    /// <summary>
    /// pauses the decrement of time.
    /// </summary>
    void Pause()
    {
        pause = !pause;
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
            
            if (!pause)
            {
                currentTime -= 1;
                yield return new WaitForSeconds(1);
                StartCoroutine(CountDown());
            }
            else
            {
                yield return null;
                StartCoroutine(CountDown());
            }

            hud.DisplayTime();
        }
    }
}
