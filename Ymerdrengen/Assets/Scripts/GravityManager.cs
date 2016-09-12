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

    private LineRenderer line;

    /// <summary>
    /// Initialization step.
    /// </summary>
    public void Start()
    {
        gyro = this.GetComponent<GyroScript>();
        line = Camera.main.transform.parent.GetComponent<LineRenderer>();
    }
    
    /// <summary>
    /// Ran once per frame.
    /// </summary>
    public void Update()
    {
        Physics.gravity = Quaternion.Euler(new Vector3(0, Camera.main.transform.parent.eulerAngles.y, 0)) * new Vector3(gyro.Xdir, -9.8f*2f, gyro.Zdir);
        //line.SetPosition(0, new Vector3(0, 1, 0) + Camera.main.transform.parent.position);
        //line.SetPosition(1, new Vector3(0, 1, 0) + Camera.main.transform.parent.position + Physics.gravity.normalized * 3f);
        //Debug.DrawLine(new Vector3(0, 5, 0) + Camera.main.transform.parent.position, new Vector3(0, 5, 0) + Camera.main.transform.parent.position + Physics.gravity.normalized * 10f,Color.red);

    }
}
