using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeMovement : MonoBehaviour
{
    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] private float travelDistance;
    [SerializeField] private float spinVelocity = 1f;
    [SerializeField] private float moveSpeed = 1f;
    private float spinAccelerate;
    private bool reverseSpin = false;
    private Vector2 desireLocation;

    void Start()
    {
        desireLocation = new Vector2(transform.position.x, transform.position.y + travelDistance);
    }

    void Update()
    {
        SpinEffect();
    }

    void SpinEffect()
    {
        transform.position = Vector2.Lerp(transform.position, desireLocation, moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * spinAccelerate);

        if (spinAccelerate >= 5f)
        {
            reverseSpin = true;

        }

        if (reverseSpin)
        {
            spinAccelerate -= spinVelocity * Time.deltaTime;
            if (spinAccelerate <= 0f)
            {
                reverseSpin = false;

            }
        }
        else
        {
            spinAccelerate += spinVelocity * Time.deltaTime;
        }
    }

}
