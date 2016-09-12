using UnityEngine;
using System.Collections;

public class SpawnYoghurt : MonoBehaviour {

    public GameObject ymer;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn()
    {
        Instantiate(ymer, ymer.transform.position, Quaternion.identity);

    }
}
