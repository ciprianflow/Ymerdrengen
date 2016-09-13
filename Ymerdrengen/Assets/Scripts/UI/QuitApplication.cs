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
        // kill process
#if UNITY_STANDALONE
        
        System.Diagnostics.Process.GetCurrentProcess().Kill();

        Application.Quit();
#endif

#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
