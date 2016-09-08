using UnityEngine;
using System.Collections;

public class calibrationscript : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RestartFIUCKYOUGEORGE() {
        Application.LoadLevel(0);
        Destroy(gameObject);
    }
}
