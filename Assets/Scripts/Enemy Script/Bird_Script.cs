using System.Runtime.Versioning;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Script : MonoBehaviour
{
    private Rigidbody2D bird;
    private Animator animator;
    private Vector3 movement =  Vector3.left;
    private Vector3 OriginalPosition;
    private Vector3 MovePosi;
    public GameObject stone;
    public LayerMask player_layer;
    private bool can_Move;
    private bool attacked = false;

    private void Awake()
    {
        bird = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        can_Move = true;
        OriginalPosition = transform.position;
        OriginalPosition.x += 6f;
        MovePosi = transform.position;
        MovePosi.x -= 6f;
        can_Move = true;
    }
    
    void Start()
    {
        
    }
    void Update()
    {
        Movement_bird();
        Drop_The_Egg();
    }

    void Movement_bird(){
        if(can_Move){
            transform.Translate(movement * Time.smoothDeltaTime);
            if(transform.position.x >= OriginalPosition.x){
                movement = Vector3.left;
                Change_direction();
            }
            else if(transform.position.x <= MovePosi.x){
                movement = Vector3.right;
                Change_direction();
            }
        }
    }
    void Change_direction(){
        Vector3 tempscale = transform.localScale;
        tempscale.x *= -1;
        transform.localScale = tempscale;
    }

    void Drop_The_Egg(){
        if(!attacked){
            if(Physics2D.Raycast(transform.position , Vector2.down , Mathf.Infinity , player_layer )){
                Instantiate(stone , new Vector3(transform.position.x , transform.position.y - 1f , transform.position.z) , Quaternion.identity);
                attacked = true;
                animator.Play("Bird Fly Animation");
            }
        }
    }

    IEnumerator Bird_dead(){
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "bullet"){
            animator.Play("Bird Dead Animation");
            GetComponent<BoxCollider2D>().isTrigger = true;
            bird.bodyType = RigidbodyType2D.Dynamic;
            can_Move = false;
            StartCoroutine(Bird_dead());
        }
    }
}
