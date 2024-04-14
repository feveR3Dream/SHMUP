using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletStatic : MonoBehaviour
{
    [Header("References")]
    /* GameObjects */
    [SerializeField] GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    void Update()
    {
        Destroy(gameObject, 6f);
    }
}
