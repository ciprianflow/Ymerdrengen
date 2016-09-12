using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Animator CamAnimator;
    public 
    // Use this for initialization
    void Start () {
        CamAnimator = GetComponent<Animator>();
        CamAnimator.SetTrigger("GameStarted");
    }
	
	// Update is called once per frame
	void Update () {
        if (this.CamAnimator.GetCurrentAnimatorStateInfo(0).IsName("CameraStart"))
        {
            Debug.Log("camera move!");
            CamAnimator.SetTrigger("CamIsMoving");
        }


    }
}
