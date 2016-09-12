using UnityEngine;
using System.Collections;

public class asdawdcaw : MonoBehaviour
{

    public Animator CamAnimator;
    bool animBool;
    // Use this for initialization
    void Start()
    {
        CamAnimator = GetComponent<Animator>();
        CamAnimator.SetTrigger("GameStarted");


    }

    // Update is called once per frame
    void Update()
    {
        animBool = CamAnimator.GetCurrentAnimatorStateInfo(0).IsName("CameraStart");
        //Debug.Log(animBool);
        if (CamAnimator.GetCurrentAnimatorStateInfo(0).IsName("CameraStart"))
        {
            //Debug.Log("camera move!");
            CamAnimator.SetTrigger("CamIsMoving");
        }


    }
}
