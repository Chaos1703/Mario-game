using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    public GameObject Bullet;
    void Update()
    {
        Shoot_Bullet();
    }

    void Shoot_Bullet(){
        if(Input.GetKeyDown(KeyCode.G)){
            GameObject bullet = Instantiate(Bullet, transform.position, UnityEngine.Quaternion.identity);
            bullet.GetComponent<Bullet_Script>().Speed *= transform.localScale.x;   // for direction as scale for right is (1,1,1) and left is (-1,1,1)
        }
    }
}