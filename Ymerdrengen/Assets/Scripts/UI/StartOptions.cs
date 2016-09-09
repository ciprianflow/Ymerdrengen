// <copyright file="StartOptions.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// Main class for the main menu 
/// </summary>
public class StartOptions : MonoBehaviour
{
    /// <summary>
    /// config start scene
    /// </summary>
    public int SceneToStart = 1;

    /// <summary>
    /// change scenes after pressing start
    /// </summary>
    public bool ChangeScenes = true;

    /// <summary>
    /// change music at start
    /// </summary>
    public bool ChangeMusicOnStart = true;

    /// <summary>
    /// check if in main menu or not
    /// </summary>
    [HideInInspector]
    public bool InMainMenu = true;

    /// <summary>
    /// audio component
    /// </summary>
    private PlayMusic music;

    /// <summary>
    /// UI Panels
    /// </summary>
    private ShowPanels showPanels;

    /// <summary>
    /// initialize components
    /// </summary>
    public void Awake()
    {
        showPanels = GetComponent<ShowPanels>();
        music = GetComponent<PlayMusic>();
        
    }

    public void OpenMainMenuOnclick()
    {
        showPanels.HideMenutitle();
        showPanels.ShowMenuPanel();
        music.PlaylevelMusic();
    }

    /// <summary>
    /// start button clicked -> load scene
    /// </summary>
    public void StartButtonClicked()
    {
        showPanels.HideMenuPanel();

        //if change scence is true
        if (ChangeScenes)
        {
            LoadGame();
        }

        // show buttons on game UI
        showPanels.ShowGameButtons();
    }

    /// <summary>
    /// Load the game
    /// </summary>
    public void LoadGame()
    {
        //disable main menu after starting the game
        InMainMenu = false;

        SceneManager.LoadScene(SceneToStart);
        
    }

    /// <summary>
    /// change music according to level
    /// </summary>
    public void OnLevelWasLoaded()
    {
        if (ChangeMusicOnStart)
        {
            music.PlaylevelMusic();
        }
    }
}
