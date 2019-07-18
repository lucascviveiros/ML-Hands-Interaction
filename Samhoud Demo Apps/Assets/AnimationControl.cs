using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Animator[] animator;
    private AnimatorStateInfo animatorStateInfo;

    void Start()
    {
        //Insect
        animator[0] = this.GetComponent<Animator>();
        animator[1] = this.GetComponent<Animator>();

        //Face
        animator[2] = this.GetComponent<Animator>();

        //Astronaut
        animator[3] = this.GetComponent<Animator>();

        //WKZ
        animator[4] = this.GetComponent<Animator>();
        animator[5] = this.GetComponent<Animator>();

        //Goather
        animator[6] = this.GetComponent<Animator>();

        //pinpin
        animator[7] = this.GetComponent<Animator>();


//        animator[0].enabled = true;

        foreach (Animator anim in animator)
        {
            anim.enabled = true;
        }
    }

    public void setAnimation(bool set, int position) 
    {
        //Insect
        if(position == 0) 
        {
            animator[position].enabled = set;
            animator[position + 1].enabled = set;
        }
        //WKZ
        else if (position == 3) 
        {
            animator[position].enabled = set;
            animator[position + 1].enabled = set;
        }
        else 
        {
            animator[position].enabled = set;
        }
    }
}
