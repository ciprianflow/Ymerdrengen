using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public Animator CamAnimator;
    public Animator BoyAnimator;
    // Use this for initialization
    void Start () {
        CamAnimator = GetComponent<Animator>();
        CamAnimator.SetTrigger("GameStarted");
        BoyAnimator = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(BoyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (BoyAnimator.GetCurrentAnimatorStateInfo(0).IsName("CameraStart") && (BoyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f))
        {
            CamAnimator.SetTrigger("CamIsMoving");
        }
    }
}
