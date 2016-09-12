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
      
        if (BoyAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleBase"))
        {
            CamAnimator.SetTrigger("CamIsMoving");
        }
    }
}
