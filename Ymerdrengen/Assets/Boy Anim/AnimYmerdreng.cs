using UnityEngine;
using System.Collections;

public class AnimYmerdreng : MonoBehaviour {

    AnimatorClipInfo[] animationClip;
    public Animator anim;
    //int walk = Animator.StringToHash("WalkCycle");
    //int noymer = Animator.StringToHash("NoYmer");
    public float currentFrame;
    public Animation WalkAnim;
    //float timer;
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        //anim.Stop();
        //timer = 0;
        anim.SetBool("isIdle", true);
        //Time.timeScale = 0.1f;

    }

    // Update is called once per frame
    void Update() {

        //For testing purposes only
        /*        
        if (Input.GetKey(KeyCode.Space))
        {
            setNoYmer();
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            setCareful();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            setWalking();
        }
        else
        {
            setIdle();
        }
        */


    }

    public void setIdle()
    {
        clearBools();
        anim.SetBool("isIdle", true);
    }
    public void setWalking()
    {
        clearBools();
        anim.SetBool("isWalking", true);
    }
    public void setCareful()
    {
        clearBools();
        anim.SetBool("isCareful", true);
    }
    public void setNoYmer()
    {
        clearBools();
        anim.SetBool("isNoYmer", true);
    }

    public void clearBools()
    {
        anim.SetBool("isIdle",    false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isCareful", false);
        anim.SetBool("isNoYmer",    false);
    }

}
