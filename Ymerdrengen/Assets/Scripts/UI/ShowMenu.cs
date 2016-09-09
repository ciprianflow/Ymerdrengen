// <copyright file="ShowMenu.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;

/// <summary>
/// Display's main menu after the first interaction
/// </summary>
public class ShowMenu : MonoBehaviour
{
    /// <summary>
    /// UI Panels
    /// </summary>
    private ShowPanels showPanels;

    /// <summary>
    /// audio script
    /// </summary>
    private PlayMusic music;

    /// <summary>
    /// initialization of components
    /// </summary>
    public void Awake()
    {
        showPanels = GetComponent<ShowPanels>();
        music = GetComponent<PlayMusic>();
    }

    /// <summary>
    /// opens the menu at first click
    /// </summary>
    public void Update () {

        /// show menu at first click
        if (Input.GetMouseButtonDown(0))
        {
            showPanels.ShowMenuPanel();
            music.PlaylevelMusic();
            Behaviour.Destroy(this);
        }
    }
}
