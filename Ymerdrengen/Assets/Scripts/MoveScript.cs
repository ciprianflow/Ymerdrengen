// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

/// <summary>
/// This script controls the movement of the player. When a
/// specific sound is played, only then will the player move
/// </summary>
public class MoveScript : MonoBehaviour
{
    /// <summary>
    /// Speed of the movement
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The path for the player
    /// </summary>
    public BezierSpline Path;

    /// <summary>
    /// Start and end positions
    /// </summary>
    private Transform StartPos, EndPos;

    /// <summary>
    /// The actual start and end positions
    /// </summary>
    private Vector3 actualStartPos, actualEndPos;

    /// <summary>
    /// GameObject girl component
    /// </summary>
    private GameObject girl;

    /// <summary>
    /// Length of the track
    /// </summary>
    private float trackLength;

    /// <summary>
    /// How far we are along the track
    /// </summary>
    private float timeTravelled;

    /// <summary>
    /// Yoghurt detection script
    /// </summary>
    private YoghurtDetection yoghurtDetection;

    /// <summary>
    /// Getting the components and initialize start and end positions
    /// </summary>
    void Start()
    {
        girl = GameObject.FindGameObjectWithTag("Girl");
        yoghurtDetection = transform.FindChild("YoghurtDetection").GetComponent<YoghurtDetection>();
        PlayerTracking();
    }

    /// <summary>
    /// Checks if the specific audio is being played, if yes 
    /// then the player moves along the track
    /// </summary>
    void FixedUpdate()
    {
        if (girl.GetComponent<AudioSource>().isPlaying && yoghurtDetection.CanMove)
        {
            timeTravelled += Time.deltaTime;
            float t = (timeTravelled * Speed) / trackLength;
            transform.position = Path.GetPoint(t);
            transform.root.LookAt(transform.position + Path.GetDirection(t));
            transform.Rotate(0, 90, 0);
            //transform.position = Vector3.Lerp(actualStartPos, actualEndPos, (timeTravelled * Speed) / trackLength);
        }
    }

    /// <summary>
    /// Sets the start and end position while finding the length of the track
    /// </summary>
    void PlayerTracking()
    {
        //actualStartPos = StartPos.position;
        actualStartPos = Path.GetPoint(0f);
        //actualEndPos = EndPos.position;
        actualEndPos = Path.GetPoint(1f);
        //trackLength = ((actualEndPos - actualStartPos).magnitude);
        trackLength = Path.GetSplineLength();
    }
}
