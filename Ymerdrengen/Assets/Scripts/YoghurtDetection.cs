using UnityEngine;
using System.Collections;

public class YoghurtDetection : MonoBehaviour
{
    public bool CanMove = true;

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
}
