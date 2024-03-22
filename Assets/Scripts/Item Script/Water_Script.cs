using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Script : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            // player death;
        }
    }
}
