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

    public static StartOptions Instance;

    public void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            showPanels = GetComponent<ShowPanels>();
            music = GetComponent<PlayMusic>();

            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    /// <summary>
    /// init coroutine for opening main menu
    /// </summary>
    public void Start()
    {
        // open main menu after MainMenuDelay delay
        StartCoroutine(OpenMainMenu());
    }

    /// <summary>
    /// Open main menu after delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator OpenMainMenu()
    {
        yield return new WaitForSeconds(MainMenuDelay);

        showMainMenu();
    }

    /// <summary>
    /// show main menu
    /// </summary>
    private void showMainMenu()
    {
        showPanels.HideMenuTitle();

        // maybe fade in..
        showPanels.ToggleBackground(true);

        showPanels.ShowMenuPanel();
        music.PlayMenuMusic();
    }

    /// <summary>
    /// start button clicked -> load scene
    /// </summary>
    public void StartButtonClicked()
    {
        showPanels.HideMenuPanel();
        showPanels.ToggleBackground(false);

        music.StopPlayMusic();
        LoadGame();

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
    /// back to main menu from in game 
    /// </summary>
    public void BackToMainMenu()
    {
        InMainMenu = true;

        showPanels.HidePausePanel();
        showPanels.ShowMenuPanel();
        showPanels.ToggleBackground(true);
        showPanels.HideGameButtons();
        music.PlayMenuMusic();

        // scene 0 is the main menu (HARDOCODED)
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// show current level text and init coroutine to hide it after LevelDelay
    /// </summary>
    public void OnLevelWasLoaded()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        // set text to label from the level array (only if in the game)
        if (currentLevel > 0)
        {
            TextLevel.text = LevelStartText[currentLevel - 1];
            showPanels.ToggleLevelTitle(true);

            StartCoroutine(DisplayLevelText());
        }
        
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

    /// <summary>
    /// Exit application
    /// </summary>
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        { 
            Application.Quit();
        }
    }

}
