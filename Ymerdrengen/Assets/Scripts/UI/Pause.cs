// <copyright file="Pause.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;

/// <summary>
/// This script handles the pause during the game
/// </summary>
public class Pause : MonoBehaviour
{
    /// <summary>
    /// UI panels
    /// </summary>
    private ShowPanels showPanels;

    /// <summary>
    /// game paused/un paused
    /// </summary>
    private bool isPaused;

    /// <summary>
    /// main script
    /// </summary>
    private StartOptions startScript;

    /// <summary>
    /// play music script
    /// </summary>
    private PlayMusic music;

    /// <summary>
    /// Initialize components
    /// </summary>
    public void Awake()
    {
        showPanels = GetComponent<ShowPanels>();
        startScript = GetComponent<StartOptions>();
        music = GetComponent<PlayMusic>();
    }

    /// <summary>
    /// Escape/back should open/close the pause panel
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !startScript.InMainMenu)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && !startScript.InMainMenu)
        {
            UnPauseGame();
        }
    }

    /// <summary>
    /// Handles pauses
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        //pause music too
        music.PauseMusicToggle(true);
        Time.timeScale = 0;

        showPanels.ShowPausePanel();
    }

    /// <summary>
    /// Handles un pauses
    /// </summary>
    public void UnPauseGame()
    {
        isPaused = false;
        // release music
        music.PauseMusicToggle(false);
        Time.timeScale = 1;

        showPanels.HidePausePanel();
    }
}
