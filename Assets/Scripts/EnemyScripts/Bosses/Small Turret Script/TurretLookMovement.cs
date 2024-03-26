using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;

public class MoveDown : MonoBehaviour
{
    public float speedRotation; 
    //public float speedForward;
    public float desiredTime;
    public float degreeAngleRight;
    public float degreeAngleLeft;


    float time; // Creating a timer

    bool turnRight;

    Quaternion right;
    Quaternion left;

    void Start()
    {
        turnRight = false;
    }

    void Update()
    {
        time+= Time.deltaTime;
        right = Quaternion.Euler(0f, 0f, degreeAngleRight);
        left = Quaternion.Euler(0f, 0f, degreeAngleLeft);

        if (turnRight)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, right, speedRotation * Time.deltaTime);
        }
        else if (!turnRight)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, left, speedRotation * Time.deltaTime);
        }
        

        if (time >= desiredTime)
        {
            turnRight = !turnRight;
            time = 0;
        }
        /*
        Vector2 movement = (Vector2)transform.position + (Vector2)transform.up * speedForward * Time.deltaTime;
        transform.position = movement;
        */
    }
}
