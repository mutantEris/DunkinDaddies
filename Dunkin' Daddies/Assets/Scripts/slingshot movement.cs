using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class slingshotmovement : MonoBehaviour
{
    public GameObject slingshot_gameobj;
    public Rigidbody2D slingshot_pouch;
    public KeyCode rotate_right;
    public KeyCode rotate_left;
    public KeyCode pull_back;
    public int start_rotation;
    public int rotation_max;
    public int rotate_speed;
    float current_rotation;
    public int initial_pull_force;
    public int pull_force_increase_speed;
    float current_pull_force;
    public int pull_force_max;
    public line_rendering slingshot_line_1;
    public line_rendering slingshot_line_2;
    bool ball_is_locked_in;
    public GameObject ball;
    public GameObject ball_spawner;
    public GameObject pouch;
    FixedJoint2D lock_joint;
    void Start()
    {
        slingshot_gameobj.transform.Rotate(Vector3.back, start_rotation);
        current_rotation = start_rotation;
        ball_is_locked_in = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(rotate_right) && current_rotation <= rotation_max){
            slingshot_gameobj.transform.Rotate(Vector3.back, rotate_speed * Time.deltaTime);
            current_rotation += rotate_speed * Time.deltaTime;
        }
        if(Input.GetKey(rotate_left) && current_rotation >= -rotation_max){
            slingshot_gameobj.transform.Rotate(Vector3.back, -rotate_speed * Time.deltaTime);
            current_rotation -= rotate_speed * Time.deltaTime;
        }
        if(Input.GetKey(pull_back)){
            if(current_pull_force < pull_force_max){
                slingshot_pouch.AddForce(-slingshot_gameobj.transform.up * current_pull_force);
                current_pull_force += (pull_force_increase_speed * Time.deltaTime);
            }
            if(!ball_is_locked_in){
                GameObject new_ball = Instantiate(ball, ball_spawner.transform.position, Quaternion.identity, null);
                lock_joint = new_ball.AddComponent<FixedJoint2D>();
                lock_joint.connectedBody = pouch.GetComponent<Rigidbody2D>();
                ball_is_locked_in = true;
            }

        }
        else{
            current_pull_force = initial_pull_force;
            if(ball_is_locked_in){
                Destroy(lock_joint);
                ball_is_locked_in = false;
            }
        }
        Debug.Log(current_pull_force / pull_force_max);
        Color line_color = new Color(current_pull_force / pull_force_max , 0, 0, 1);
        slingshot_line_1.myLineRenderer.startColor = line_color;
        slingshot_line_2.myLineRenderer.startColor = line_color;
    }
}
