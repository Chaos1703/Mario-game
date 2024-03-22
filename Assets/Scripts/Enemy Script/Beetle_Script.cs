using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle_Script : MonoBehaviour
{
    public LayerMask playerlayer;
    public float Movespeed = 2f;
    private Rigidbody2D beetle;
    private Animator animator;
    private bool Moveleft;
    private bool canMove;
    private bool stunned;
    public Transform down_Collision , left_Collision , right_Collision , top_Collision;
    private Vector3 left_Collision_position , right_Collision_position;

    private void Awake()
    {
        beetle = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Moveleft = true;
        left_Collision_position = left_Collision.position;
        right_Collision_position = right_Collision.position;
        canMove = true;
    }

    void Update()
    {
        if(canMove){
            if(Moveleft){
                beetle.velocity = new Vector2(-Movespeed , beetle.velocity.y);
            }
            else{
                beetle.velocity = new Vector2(Movespeed , beetle.velocity.y);
            }
        }
        check();
    }

    void check(){
        if(!Physics2D.Raycast(down_Collision.position , Vector2.down , 0.1f)){
            Moveleft = !Moveleft;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Vector3 temp = left_Collision_position;
            left_Collision_position = right_Collision_position;
            right_Collision_position = temp;   
        }
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision_position , Vector2.left , 0.1f , playerlayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision_position , Vector2.right , 0.1f , playerlayer);
        Collider2D tophit = Physics2D.OverlapCircle(top_Collision.position , 0.2f , playerlayer);
        if(tophit && tophit.gameObject.tag == "Player" && !stunned){
            tophit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(tophit.gameObject.GetComponent<Rigidbody2D>().velocity.x , 7f);
            canMove = false;
            beetle.velocity = new Vector2(0f , 0f);
            animator.Play("Beetle Stunned Animation");
            stunned = true;
            StartCoroutine(dead(0.5f));
        }
        if(leftHit && leftHit.collider.gameObject.tag == "Player"){
            leftHit.collider.gameObject.GetComponent<Player_Damage>().Damage();
        }
        if((rightHit && rightHit.collider.gameObject.tag == "Player") && !stunned){
            rightHit.collider.gameObject.GetComponent<Player_Damage>().Damage();
        }
    }
    IEnumerator dead(float timer){
        yield return new WaitForSeconds (timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!stunned){
            animator.Play("Beetle Stunned Animation");
        }
        canMove = false;
        stunned = true;
        beetle.velocity = new Vector2(0f , 0f);
        StartCoroutine(dead(0.4f));
    }
}

