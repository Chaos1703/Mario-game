using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Script : MonoBehaviour
{
    private Animator animator;
    private bool animation_started;
    private bool animation_finished;
    private int jump_Times;
    private bool jump_left = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(Frog_Jump());
    }
    void Update()
    {
        
    }
    void LateUpdate()
    {
        if(animation_finished && animation_started){
            animation_started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }
    IEnumerator Frog_Jump(){
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f , 4f));
        jump_Times++;
        animation_started = true;
        animation_finished = false;
        if(jump_left){
            animator.Play("Frog Jump Left Animation");
        }
        else{
            animator.Play("Frog Jump Right Animation");
        }
        StartCoroutine(Frog_Jump());
    }

    void Animation_Finished(){
        animation_finished = true;
        if(jump_left)
            animator.Play("Frog Idle Left Animation");
        else
            animator.Play("Frog Idle Right Animation");
        if(jump_Times == 3){
            jump_Times = 0;
            Vector3 tempscale = transform.localScale;
            tempscale.x*=-1;
            transform.localScale = tempscale;
            jump_left = !jump_left;
        }
    }
}
