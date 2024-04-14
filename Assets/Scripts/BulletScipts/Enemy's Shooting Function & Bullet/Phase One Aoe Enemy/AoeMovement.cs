using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeMovement : MonoBehaviour
{
    [Header("Values")]
    /* Floats and Ints */
    public float spinVelocity = 10f;

    void Start()
    {
        StartCoroutine(SpinningBullet());
    }

    IEnumerator SpinningBullet()
    {
        while (true)
        {
            transform.Rotate(Vector3.forward * spinVelocity * Time.deltaTime);
            yield return null;
        }
    }
}
