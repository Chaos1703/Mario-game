using System.Globalization;
using System.ComponentModel;
using System.Timers;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Damage : MonoBehaviour
{
    private Text lives;
    private int life_score_count;
    private bool can_damage;

    void Awake()
    {
        lives = GameObject.Find("Lives").GetComponent<Text>();
        life_score_count = 3;
        lives.text = "X" + life_score_count;
        can_damage = true;
    }
    
    public void Damage(){
        if(can_damage){
            life_score_count--;
            if(life_score_count >= 0){
                lives.text = "X" + life_score_count;
                print("player recieved damage");
            }
            if(life_score_count == 0){
                Time.timeScale = 0;
                StartCoroutine(Restart());
            }
            can_damage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage(){
        yield return new WaitForSeconds(2f);
        can_damage = true;
    }

    IEnumerator Restart(){
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("Gameplay");
    }
}
