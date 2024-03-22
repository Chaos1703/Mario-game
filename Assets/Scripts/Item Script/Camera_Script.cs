using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public float reset_speed = 0.5f;
    public float camera_speed = 0.3f;
    public Bounds camera_bounds;
    private Transform player;
    private float offsetz;
    private Vector3 last_player_position;
    private Vector3 currentVelocity;
    private bool follows_player;

    void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(Camera.main.aspect*2f*Camera.main.orthographicSize , 15f);
        camera_bounds = collider.bounds;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        last_player_position = player.position;
        offsetz = (transform.position - player.position).z;
        follows_player = true;
    }

    void FixedUpdate()
    {
        if(follows_player){
            Vector3 ahead_player_pos = player.position + Vector3.forward*offsetz;
            if(ahead_player_pos.x >= transform.position.x){
                Vector3 new_camera_pos = Vector3.SmoothDamp(transform.position , ahead_player_pos , ref currentVelocity , camera_speed);
                transform.position = new Vector3 (new_camera_pos.x , transform.position.y , new_camera_pos.z);
                last_player_position = player.position;
            }
        }
    }
}
