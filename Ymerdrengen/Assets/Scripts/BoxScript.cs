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
    /// 
    /// </summary>
    public GameObject box;

    /// <summary>
    /// 
    /// </summary>
    public GameObject endPoint;

    /// <summary>
    /// 
    /// </summary>
    public float FallSpeed = 1f;
    public float RotateSpeed = 1f;


    Vector3 stopRotating = new Vector3(0, 0, 90);


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (!GyroScript.isShaked && BoxDetection.isFalling)
        {

            if (box.transform.eulerAngles.z < 90)
            {

                Debug.Log(box.transform.eulerAngles.z);
                box.transform.Rotate(0, 0, Time.deltaTime * RotateSpeed);
            }
            // if (Vector3.Distance(transform.eulerAngles, stopRotating) > 0.01f)
            // {
            //     transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, stopRotating, Time.deltaTime);

            // }
            //else
            //{
            //     transform.eulerAngles = stopRotating;
            // }

            box.transform.position = Vector3.Lerp(box.transform.position, endPoint.transform.position, FallSpeed * Time.deltaTime);
        }
    }
}
