using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDown : MonoBehaviour
{
    public float speedDown;
    public float speedSides;
    public float desiredTime;

    float time; // Creating a timer

    public GameObject cube;

    bool turnDir;

    void Start()
    {
        turnDir = true;
    }

    void Update()
    {
        time+= Time.deltaTime;

        if (time >= desiredTime)
        {
            turnDir = !turnDir;
            time = 0;
        }

        Vector2 movement = new Vector2(turnDir ? -speedSides : speedSides, speedDown) * Time.deltaTime;
        cube.transform.position = movement;
    }
}
