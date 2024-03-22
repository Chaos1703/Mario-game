using System.ComponentModel;
using System.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private bool isGrounded;
    private bool jumped;
    public float jump_power = 14f;
    public float speed = 5f;
    private Rigidbody2D player;
    private Animator animator;
    public Transform check_posi;
    public LayerMask ground_layer;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        check_ground();
        player_jump();
    }
    private void FixedUpdate()
    {
        playerwalk();
    }
    void playerwalk(){
        float direction = Input.GetAxisRaw("Horizontal");
        if(direction > 0){
            player.velocity = new Vector2(speed , player.velocity.y);
            body_direction(1);
        }
        else if(direction < 0){
            player.velocity = new Vector2(-speed , player.velocity.y);
            body_direction(-1);
        }
        else{
            player.velocity = new Vector2(0 , player.velocity.y);
        }
        animator.SetInteger("speed" , Mathf.Abs((int)player.velocity.x));
    }

    void body_direction(int d){
        Vector3 tempscale = transform.localScale;
        tempscale.x = d;
        transform.localScale = tempscale;
    }
    
    void check_ground(){
        isGrounded = Physics2D.Raycast(check_posi.position , Vector2.down , 0.1f , ground_layer);
        if(isGrounded && jumped){
            jumped = false;
            animator.SetBool("jump" , false);
        }
    }
    void player_jump(){
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            jumped = true;
            player.velocity = new Vector2(player.velocity.x , jump_power);
            animator.SetBool("jump" , true);
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Z)){
            jumped = true;
            player.velocity = new Vector2(player.velocity.x , jump_power*2);
            animator.SetBool("jump" , true);
        }
    }
}
