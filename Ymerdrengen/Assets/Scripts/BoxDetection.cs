using UnityEngine;
using System.Collections;

public class BoxDetection : MonoBehaviour {


    private Vector3 center;
    public float radius;
    public static bool isFalling;

    /// <summary>
    /// GameObject ymerdrengen component
    /// </summary>
    private GameObject ydreng;

    // Use this for initialization
    void Start () {
        ydreng = GameObject.FindGameObjectWithTag("Ymerdrengen");

        center = this.transform.position;
        radius = 2.0f;
        isFalling = false;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void OnTriggerEnter(Collider collision)
    {
        if(collision == ydreng)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                isFalling = true;
                hitColliders[i].SendMessage("Box falls");
                i++;
            }

        }
    }

}
