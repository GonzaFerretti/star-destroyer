using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour {
    public Animator anim;
    public bool firstboot;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        firstboot = false;
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKeyDown("w") || (Input.GetKeyDown("s")) || (Input.GetKeyDown("a")) || (Input.GetKeyDown("d"))))
        { if ((firstboot == false))
            {
                firstboot = true;
                anim.Play("start");
            }
            else anim.Play("speed up");
        }
        if ((Input.GetKeyUp("w") || (Input.GetKeyUp("s")) || (Input.GetKeyUp("a")) || (Input.GetKeyUp("d"))))
        {
            anim.Play("slowing down");
        }
    }
}
