// <copyright file="ShowPanels.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;

/// <summary>
/// UI Panel handler
/// </summary>
public class ShowPanels : MonoBehaviour
{
    /// <summary>
    /// Menu Panel
    /// </summary>
    public GameObject MenuPanel;

    /// <summary>
    /// Tint used for animations menu -> game
    /// </summary>
    public GameObject OptionsTint;

    /// <summary>
    /// Options Panel
    /// </summary>
    public GameObject OptionsPanel;

    /// <summary>
    /// Pause panel (in game)
    /// </summary>
    public GameObject PausePanel;

    /// <summary>
    /// Restart level button
    /// </summary>
    public GameObject RestartLevelButton;

    /// <summary>
    /// Pause game button
    /// </summary>
    public GameObject PauseGameButton;

    /// <summary>
    /// Menu title
    /// </summary>
    public GameObject MenuTitle;

    /// <summary>
    /// Level Title
    /// </summary>
    public GameObject LevelTitle;

    public GameObject Background;

    /// <summary>
    /// Hide Title 
    /// </summary>
    public void HideMenutitle()
    {
        MenuTitle.SetActive(false);
    }

    /// <summary>
    /// show menu
    /// </summary>
    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
    }

    /// <summary>
    /// hide menu
    /// </summary>
    public void HideMenuPanel()
    {
        MenuPanel.SetActive(false);
    }

    /// <summary>
    /// show options
    /// </summary>
    public void ShowOptionsPanel()
    {
        OptionsTint.SetActive(true);
        OptionsPanel.SetActive(true);
    }

    /// <summary>
    /// hide options
    /// </summary>
    public void HideOptionsPanel()
    {
        OptionsTint.SetActive(false);
        OptionsPanel.SetActive(false);
    }

    /// <summary>
    /// show pause
    /// </summary>
    public void ShowPausePanel()
    {
        OptionsTint.SetActive(true);
        PausePanel.SetActive(true);
    }

    /// <summary>
    /// hide pause
    /// </summary>
    public void HidePausePanel()
    {
        OptionsTint.SetActive(false);
        PausePanel.SetActive(false);
    }

    /// <summary>
    /// show game buttons 
    /// </summary>
    public void ShowGameButtons()
    {
        RestartLevelButton.SetActive(true);
        PauseGameButton.SetActive(true);
    }

    /// <summary>
    /// hide game buttons
    /// </summary>
    public void HideGameButtons()
    {
        RestartLevelButton.SetActive(false);
        PauseGameButton.SetActive(false);
    }

    public void ToggleLevelTitle(bool value)
    {
        LevelTitle.SetActive(value);
    }
    
    public void ToggleBackground(bool value)
    {
        Background.SetActive(value);
    }

}
