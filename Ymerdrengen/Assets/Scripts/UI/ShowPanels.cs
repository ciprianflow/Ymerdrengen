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
}
