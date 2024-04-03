using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOneBasicHealth : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] int health = 20;
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            health--;
        }

    }

    void Update()
    {
        if (health < 1)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
