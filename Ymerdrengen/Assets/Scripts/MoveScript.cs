// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum States
{
    MovingForward,
    MovingBack,
    Turning,
    StandingStill,
    Idle,
}

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



    private States characterState;
    public States CharacterState
    {
        get { return characterState; }
        set { characterState = value; }
    }

    private States lastMovementDirection;

    private bool wasBlocked = false;

    private bool endOfPath = false;

    private BezierSpline currentSpline;

    private float turningThreshold = 15f;

    private const float internalThreshold = 1f;

    private Vector3 nextDirection;

    private AudioSource girlAudio;

    private float pauseStart;

    private float pauseDuration = 2f;

    private bool pauseStarted = false;

    /// <summary>
    /// Getting the components and initialize start and end positions
    /// </summary>
    void Start()
    {
        if(GameObject.Find("GravityManager") == null)
        {
            characterState = States.MovingForward;
        }

        girl = GameObject.FindGameObjectWithTag("Girl");
        girlAudio = girl.GetComponent<AudioSource>();
        yoghurtDetection = transform.FindChild("YoghurtDetection").GetComponent<YoghurtDetection>();
        BFS bfs = new BFS();
        Path = new Stack<BezierSpline>();
        Path = bfs.findNearestFinalDestination(pathSystem.bezierSplines[0]);
        currentSpline = Path.Pop();

        transform.position = currentSpline.GetPoint(0);
        transform.root.LookAt(transform.position + currentSpline.GetDirection(0));

        characterState = States.Idle;
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
            default:
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
        if (girlAudio.isPlaying && yoghurtDetection.CanMove)
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
                    endOfPath = true;
                    characterState = States.StandingStill;
                    return;
                }
                currentSpline = Path.Pop();
            }
        }
        else if (girlAudio.isPlaying && !yoghurtDetection.CanMove)
        {
            //Debug.Log("MOVING BACK NOW");
            characterState = States.StandingStill;
            pauseStart = Time.time;
            pauseStarted = true;
        }
    }

    private void MoveBack()
    {
        wasBlocked = false;
        lastMovementDirection = States.MovingBack;
        if (girlAudio.isPlaying && yoghurtDetection.CanMove)
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
            if (t <= 0)
            {
                timeTravelled = 0;
                characterState = States.StandingStill;
            }
        }
        else if (girlAudio.isPlaying && !yoghurtDetection.CanMove)
        {
            characterState = States.StandingStill;
        }
    }

    private void StandStill()
    {
        if (endOfPath)
        {
            Debug.Log("DEAD");
            return;
        }
        if (pauseStarted)
        {
            Debug.Log("pause");
            if (pauseStart + pauseDuration < Time.time)
            {
                Debug.Log("switching to moving");
                characterState = States.MovingForward;
                pauseStarted = false;
            }
        }
        else
        {
        wasBlocked = true;
        float t = (timeTravelled * Speed) / trackLength;
        nextDirection = -currentSpline.GetDirection(t);
        characterState = States.Turning;
        }
    }

    private float GetAngle(Vector3 currentPos, Vector3 nextPosition)
    {
        float angle = Vector3.Angle(currentPos, nextPosition);
        if (angle >= (characterState == States.Turning ? internalThreshold : turningThreshold))
        {
            return angle;
        }
        return 0;
    }

    private void Rotate(Vector3 finalRotation, bool wasBlocked)
    {
        float angle = GetAngle(transform.forward, finalRotation);
        Debug.Log(DetermineDirection(transform.forward, finalRotation));
        if (angle > internalThreshold)
        {
            Turn(RotationSpeed * DetermineDirection(transform.forward,finalRotation));
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
        transform.Rotate(new Vector3(0, angle, 0) * Time.deltaTime);
    }

    private bool ShouldITurn(Vector3 nextDirection)
    {
        if (GetAngle(transform.forward, nextDirection) > turningThreshold)
        {
            return true;
        }
        return false;
    }

    private float DetermineDirection(Vector3 referenceForward, Vector3 newDirection)
    {
         Vector3 referenceRight = Vector3.Cross(Vector3.up, referenceForward);
         float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));
         return sign;
    }
}
