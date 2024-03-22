using System.Collections.Specialized;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Script : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D spider;
    private Vector3 move_Direction = Vector3.down;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spider = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(Change_Movement());
    }
    void Update()
    {
        move_body();
    }

    void move_body(){
        transform.Translate(move_Direction*Time.smoothDeltaTime);
    }

    IEnumerator Change_Movement(){
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));

        if(move_Direction == Vector3.down)
            move_Direction = Vector3.up;
        else
            move_Direction = Vector3.down;
        StartCoroutine(Change_Movement());
    }

    IEnumerator Spider_Dead(){
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "bullet"){
            animator.Play("Spider Dead Animation");
            spider.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Spider_Dead());
            StopCoroutine(Change_Movement());
        }
    }
}
