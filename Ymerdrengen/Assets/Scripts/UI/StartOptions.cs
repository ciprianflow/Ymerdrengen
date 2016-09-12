// <copyright file="StartOptions.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
    /// Main menu delay
    /// </summary>
    public float MainMenuDelay = 3f;

    /// <summary>
    /// Text label
    /// </summary>
    public Text TextLevel;

    /// <summary>
    /// Level Text display delay
    /// </summary>
    public float LevelDelay = 2f;

    /// <summary>
    /// Level Scene array
    /// </summary>
    public string[] LevelStartText;

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
    
    /// <summary>
    /// init coroutine for opening main menu
    /// </summary>
    public void Start()
    {
        StartCoroutine(OpenMainMenu());
    }

    /// <summary>
    /// Open main menu after delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenMainMenu()
    {
        yield return new WaitForSeconds(MainMenuDelay);

        showPanels.HideMenutitle();
        showPanels.ShowMenuPanel();
        music.PlayMenuMusic();
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
            music.StopPlayMusic();
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
    /// show current level text and init coroutine to hide it after delay
    /// </summary>
    public void OnLevelWasLoaded()
    {
        TextLevel.text = LevelStartText[SceneManager.GetActiveScene().buildIndex - 1];
        showPanels.ToggleLevelTitle(true);

        StartCoroutine(DisplayLevelText());
    }

    /// <summary>
    /// hide level title
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayLevelText()
    {
        yield return new WaitForSeconds(LevelDelay);

        showPanels.ToggleLevelTitle(false);
        TextLevel.text = "";
    }
}
