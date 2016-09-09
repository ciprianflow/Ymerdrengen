using UnityEngine;
using System.Collections;

public class LoadnewLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	Application.LoadLevel(Application.loadedLevel + 1); 
	}
}
