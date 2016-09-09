// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using System.Collections.Generic;
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
    public float Speed = 10;

    public float RotationSpeed = 0.5f;

    /// <summary>
    /// The path for the player
    /// </summary>
    public Stack<BezierSpline> Path;

    public pathSystem pathSystem;

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

    private enum States
    {
        MovingForward,
        MovingBack,
        Turning,
        StandingStill,
    }

    private States characterState;

    private States lastMovementDirection;

    private bool wasBlocked = false;

    private BezierSpline currentSpline;

    private float turningThreshold = 15f;

    private const float internalThreshold = 1f;

    private Vector3 nextDirection;

    /// <summary>
    /// Getting the components and initialize start and end positions
    /// </summary>
    void Start()
    {
        girl = GameObject.FindGameObjectWithTag("Girl");
        yoghurtDetection = transform.FindChild("YoghurtDetection").GetComponent<YoghurtDetection>();
        BFS bfs = new BFS();
        Path = new Stack<BezierSpline>();
        Path = bfs.findNearestFinalDestination(pathSystem.bezierSplines[0]);
        currentSpline = Path.Pop();

        transform.position = currentSpline.GetPoint(0);
        transform.root.LookAt(transform.position + currentSpline.GetDirection(0));

        characterState = States.MovingForward;
        PlayerTracking();
    }

    /// <summary>
    /// Checks if the specific audio is being played, if yes 
    /// then the player moves along the track
    /// </summary>
    void LateUpdate()
    {

        switch (characterState)
        {
            case States.MovingForward:
                //Debug.Log("moving forward");
                MoveForward();
                break;
            case States.Turning:
                //Debug.Log("turning");
                Rotate(nextDirection, wasBlocked);
                break;
            case States.MovingBack:
                //Debug.Log("moving back");
                MoveBack();
                break;
            case States.StandingStill:
                //Debug.Log("standing still");
                StandStill();
                break;
        }
    }

    /// <summary>
    /// Sets the start and end position while finding the length of the track
    /// </summary>
    void PlayerTracking()
    {
        //actualStartPos = StartPos.position;
        actualStartPos = currentSpline.GetPoint(0f);
        //actualEndPos = EndPos.position;
        actualEndPos = currentSpline.GetPoint(1f);
        //trackLength = ((actualEndPos - actualStartPos).magnitude);
        trackLength = currentSpline.GetSplineLength();
    }

    private void MoveForward()
    {
        wasBlocked = false;
        lastMovementDirection = States.MovingForward;
        if (girl.GetComponent<AudioSource>().isPlaying && yoghurtDetection.CanMove)
        {
            timeTravelled += Time.deltaTime;
            float t = (timeTravelled * Speed) / trackLength;
            nextDirection = currentSpline.GetDirection(t);

            if (ShouldITurn(nextDirection))
            {
                characterState = States.Turning;
                return;
            }

            transform.position = currentSpline.GetPoint(t);
            transform.root.LookAt(transform.position + nextDirection);
            transform.Rotate(0, 0, 0);
            //transform.position = Vector3.Lerp(actualStartPos, actualEndPos, (timeTravelled * Speed) / trackLength);
            if (t >= 1)
            {
                timeTravelled = 0;
                if (Path.Count == 0)
                {
                    characterState = States.StandingStill;
                    return;
                }
                    currentSpline = Path.Pop();     
            }
        }
        else if (girl.GetComponent<AudioSource>().isPlaying && !yoghurtDetection.CanMove)
        {
            //Debug.Log("MOVING BACK NOW");
            characterState = States.StandingStill;
        }
    }

    private void MoveBack()
    {
        wasBlocked = false;
        lastMovementDirection = States.MovingBack;
        if (girl.GetComponent<AudioSource>().isPlaying && yoghurtDetection.CanMove)
        {
            timeTravelled -= Time.deltaTime;
            float t = (timeTravelled * Speed) / trackLength;
            nextDirection = -currentSpline.GetDirection(t);

            if (ShouldITurn(nextDirection))
            {
                characterState = States.Turning;
                return;
            }

            transform.position = currentSpline.GetPoint(t);
            transform.root.LookAt(transform.position + nextDirection);
            transform.Rotate(0, 0, 0);
            //transform.position = Vector3.Lerp(actualStartPos, actualEndPos, (timeTravelled * Speed) / trackLength);
            if (t  <= 0)
            {
                timeTravelled = 0;
                characterState = States.StandingStill;
            }
        }
        else if (girl.GetComponent<AudioSource>().isPlaying && !yoghurtDetection.CanMove)
        {
            characterState = States.StandingStill;
        }
    }

    private void StandStill()
    {
        if(Path.Count == 0)
        {
            return;
        }
        if (girl.GetComponent<AudioSource>().isPlaying && !yoghurtDetection.CanMove)
        {
            wasBlocked = true;
            float t = (timeTravelled * Speed) / trackLength;
            nextDirection = -currentSpline.GetDirection(t);
            characterState = States.Turning;
        }
        else if (girl.GetComponent<AudioSource>().isPlaying && yoghurtDetection.CanMove)
        {
            wasBlocked = true;
            float t = (timeTravelled * Speed) / trackLength;
            nextDirection = -currentSpline.GetDirection(t);
            characterState = States.Turning;
        }
        
    }

    private float? GetAngle(Vector3 currentPos, Vector3 nextPosition)
    {
        float angle = Vector3.Angle(currentPos, nextPosition);
        if (angle >= (characterState == States.Turning ? internalThreshold : turningThreshold))
        {
            return angle;
        }
        return null;
    }

    private void Rotate(Vector3 finalRotation, bool wasBlocked)
    {
        if (GetAngle(transform.forward,finalRotation) > internalThreshold)
        {
            Turn(RotationSpeed);
        }
        else
        {
            if (wasBlocked)
            {
                if (lastMovementDirection == States.MovingForward)
                {
                    characterState = States.MovingBack;
                }
                else
                {
                    characterState = States.MovingForward;
                }
            }
            else
            {
                characterState = lastMovementDirection;
            }
        }
    }

    private void Turn(float angle)
    {
        transform.Rotate(new Vector3(0,angle,0)*Time.deltaTime);
    }

    private bool ShouldITurn(Vector3 nextDirection)
    {
        if (GetAngle(transform.forward, nextDirection) > turningThreshold)
        {
            return true;
        }
        return false;
    }
}
