using System.Runtime.Versioning;
using System.Reflection;
using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg_Script : MonoBehaviour
{
     void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            // Damage Player
        }
        gameObject.SetActive(false);
    }
}
