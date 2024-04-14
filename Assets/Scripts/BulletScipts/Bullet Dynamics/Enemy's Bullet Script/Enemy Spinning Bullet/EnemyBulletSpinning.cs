using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpinning : MonoBehaviour
{
    [Header("References")]
    /* GameObjects */
    [SerializeField] GameObject hitEffect;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float spinVelocity = 10f;

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

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, 5f);
    }
}
