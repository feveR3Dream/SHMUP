using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemyMovement : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    private GameObject player;


    [Header("Values")]
    /* Floats and Ints */ 
    [SerializeField] private float moveSpeed;
    /* Booleans */
    private bool stop; // To prevent the movement of newly spawned homing planes when player dies.

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("Could not find the player GameObject");
            stop = true;
        }
        else
        {
            stop = false;
        }
    }


    void Update()
    {
        if (!stop)
        {
            Vector2 lookDir = player.transform.position - transform.position;

            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }

    }

}
