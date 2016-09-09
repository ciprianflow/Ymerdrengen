// <copyright file="QuitApplication.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;

/// <summary>
/// closes the game
/// </summary>
public class QuitApplication : MonoBehaviour
{
    /// <summary>
    /// closes the game
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
