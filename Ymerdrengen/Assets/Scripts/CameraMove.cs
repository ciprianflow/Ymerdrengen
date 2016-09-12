using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Animator CamAnimator;
    public Animator BoyAnimator;
    // Use this for initialization
    void Start () {
        CamAnimator = GetComponent<Animator>();
        CamAnimator.SetTrigger("GameStarted");

        BoyAnimator = this.GetComponentInParent<Animator>();

    }

    // Update is called once per frame
    void Update () {
        CamAnimator.Update(Time.smoothDeltaTime);
        CamAnimator.Update(Time.smoothDeltaTime);
        if (BoyAnimator.GetCurrentAnimatorStateInfo(0).IsName("CameraStart"))
        {
            Debug.Log("camera move!");
            CamAnimator.SetTrigger("CamIsMoving");
        }


    }
}
