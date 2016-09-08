// <copyright file="GravityManager.cs" company="Team4">
// Company copyright tag.
// </copyright>
using System.Collections;
using UnityEngine;

/// <summary>
/// Class for modifying internal gravity direction.
/// </summary>
[RequireComponent(typeof(GyroScript))]
public class GravityManager : MonoBehaviour
{
    /// <summary>
    /// GyroScript class that contains the gyroscope controls.
    /// </summary>
    private GyroScript gyro;

    /// <summary>
    /// Initialization step.
    /// </summary>
    public void Start()
    {
        gyro = this.GetComponent<GyroScript>();
    }
    
    /// <summary>
    /// Ran once per frame.
    /// </summary>
    public void Update()
    {
        Physics.gravity = new Vector3(gyro.Xdir, -9.8f, gyro.Zdir);
    }
}
