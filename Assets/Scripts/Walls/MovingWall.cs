using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [Header("References")]
    /* Trasnforms */ 
    [SerializeField] Transform targetPosition;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float speed = 2f;
    /* Booleans */
    private bool moved;


    [Header("Scripts")]
    private PlayOption playStatus;

    void Start()
    {
        moved = false;
        playStatus = FindObjectOfType<PlayOption>();
        if (playStatus == null)
            Debug.Log("Could not find the script");
    }

    void Update()
    {
        if (playStatus != null && playStatus.started)
        {
            if (!moved)
            {
                transform.position = Vector2.Lerp(transform.position, targetPosition.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetPosition.position) < 0.0001f)
                {
                    moved = true;
                }
            }
        }
    }
}
