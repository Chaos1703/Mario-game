using System.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score_Script : MonoBehaviour
{
    private Text Score;
    private AudioSource audio;
    private int Score_Count = 0;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Start()
    {
        Score = GameObject.Find("Coins").GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "coin"){
            Score_Count++;
            Score.text = "X" + Score_Count;
            Destroy(other.gameObject);
            audio.Play();
        }
    }
}
