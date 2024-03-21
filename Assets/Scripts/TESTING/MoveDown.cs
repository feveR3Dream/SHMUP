using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed;
    public GameObject cube;

    public float time;

    void Start()
    {
        while (true)
        {
            StartCoroutine(MoveZigzag());
        }                                                           
    }

    IEnumerator MoveZigzag()
    {
        Vector2 movementRight = (Vector2)cube.transform.position + new Vector2(speed * Time.deltaTime, -speed * Time.deltaTime); // Use these method to code the movement of anything 
        cube.transform.position = movementRight;

        yield return new WaitForSeconds(time);

        Vector2 movementLeft = (Vector2)cube.transform.position + new Vector2(-speed * Time.deltaTime, -speed * Time.deltaTime); // Use these method to code the movement of anything 
        cube.transform.position = movementLeft;
    }
}
