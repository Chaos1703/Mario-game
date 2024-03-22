using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    private float speed = 10f;
    private Animator animator;
    private bool canMove;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        canMove = true;
        StartCoroutine(Disable_Bullet(5f));
    }
    private void Update()
    {
        Move(canMove);   
    }

    void Move(bool x){
        if(x){
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "snail" || other.tag == "Beetle" || other.tag == "bird" || other.tag == "Spider"){
            animator.Play("Explode Animation");
            canMove = false;
            StartCoroutine(Disable_Bullet(0.3f));
        }
    }
    IEnumerator Disable_Bullet(float timer){
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
    public float Speed{
        get{
            return speed;
        }
        set{
            speed = value;
        }
    }
}
