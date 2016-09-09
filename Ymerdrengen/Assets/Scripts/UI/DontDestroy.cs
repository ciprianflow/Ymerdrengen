// <copyright file="DontDestroy.cs" company="Team4">
// Company copyright tag.
// </copyright>

using UnityEngine;

/// <summary>
/// This script doesn't destroy the UI game object
/// </summary>
public class DontDestroy : MonoBehaviour
{
    /// <summary>
    /// Calls don't destroy on load
    /// </summary>
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
