// <copyright file="YoghurtDetection.cs" company="Team4">
// Company copyright tag.
// </copyright>
using System.Collections;
using UnityEngine;

/// <summary>
/// Class for shitty yoghurt detection.
/// </summary>
public class YoghurtDetection : MonoBehaviour
{
    /// <summary>
    /// Can move output variable.
    /// </summary>
    public bool CanMove = true;

    /// <summary>
    /// Checking for collision with yoghurt.
    /// </summary>
    /// <param name="collision">Collision with objects in the scene</param>
    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer != 30)
        {
            CanMove = true;
        }

        if (collision.gameObject.layer == 30)
        {
            CanMove = false;
        }
    }

    /// <summary>
    /// On trigger enter function.
    /// </summary>
    /// <param name="collision">Collision with the object</param>
    public void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.layer == 29)
        {
            //Do pathfinding backwards
        }
    }
}
