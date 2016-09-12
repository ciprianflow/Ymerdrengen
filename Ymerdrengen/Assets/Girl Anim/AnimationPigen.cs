using UnityEngine;
using System.Collections;

public class AnimationPigen : MonoBehaviour {

    public Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        //anim.Stop();
        //timer = 0;
        setIdle();
        //Time.timeScale = 0.1f;

    }

    // Update is called once per frame
    void Update() {

        //For testing purposes only
        /*        
        if (Input.GetKey(KeyCode.P))
        {
            setSinging();
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
    public void setSinging()
    {
        clearBools();
        anim.SetBool("isSinging", true);
    }

    public void clearBools()
    {
        anim.SetBool("isIdle",    false);
        anim.SetBool("isSinging", false);
    }

}
