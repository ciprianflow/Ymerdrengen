// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

/// <summary>
/// This script controls the falling of the cereal boxes. When the tablet
/// has been shook the boxes falls off the shelves.
/// </summary>
public class BoxScript : MonoBehaviour {

    /// <summary>
    /// Yoghurt detection script
    /// </summary>
    private YoghurtDetection yoghurtDetection;

    /// <summary>
    /// Box detection script
    /// </summary>
    private BoxDetection boxDetection;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        yoghurtDetection = transform.FindChild("YoghurtDetection").GetComponent<YoghurtDetection>();
        boxDetection = transform.FindChild("BoxDetection").GetComponent<BoxDetection>();
    }

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (GyroScript.isShaked && BoxDetection.isFalling)
        {

        }
    }
}
