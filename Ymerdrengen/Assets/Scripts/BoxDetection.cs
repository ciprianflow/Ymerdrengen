// <copyright file="MoveScript.cs" company="Team4">
// Company copyright tag.
// </copyright>

using System.Collections;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    private Vector3 center;
    public float radius;
    public static bool isFalling;

    /// <summary>
    /// GameObject ymerdrengen component
    /// </summary>
    private GameObject[] cereal;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        cereal = GameObject.FindGameObjectsWithTag("CerealBox");

        center = this.transform.position;
        radius = 2.0f;
        isFalling = false;
    }

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        for (int i = 0; i <= cereal.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, cereal[i].transform.position) < radius)
            {
                Debug.Log("fall");
                isFalling = true;
            }
        }

            //    Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            //int i = 0;
            //while (i < hitColliders.Length)
            //{
            //Debug.Log(hitColliders[i].gameObject);
            //if (hitColliders[i].gameObject == cereal)
            //{
            //    isFalling = true;
            //    //hitColliders[i].SendMessage("Box falls");
            //    Debug.Log("hello");
            //}
                
            //    i++;
            //}

        //public void OnTriggerEnter(Collider collision)
        //{
        //    if(collision.gameObject.layer == 26)
        //    {
        //        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        //        int i = 0;
        //        while (i < hitColliders.Length)
        //        {
        //            isFalling = true;
        //            //hitColliders[i].SendMessage("Box falls");
        //            Debug.Log("hello");
        //            i++;
        //        }

        //    }
        //}

    }
}
