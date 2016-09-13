using UnityEngine;
using System.Collections;

public class LoadnewLevel : MonoBehaviour {

    public GameObject Boy;

    public float EndLevelDistance = 1f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
        // distance until the girl
        if (Vector3.Distance(Boy.transform.position, this.transform.position) < EndLevelDistance)
        {
            Application.LoadLevel(Application.loadedLevel +1);
        }

	}
}
