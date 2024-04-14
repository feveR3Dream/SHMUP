using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOneBasicHealth : MonoBehaviour
{
    [Header("References")]
    /* GameObjects */
    [SerializeField] GameObject deathEffect;

    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] int health = 20;

    private Management manager;

    void Start()
    {
        manager = FindObjectOfType<Management>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (manager.canTakeDamage)
            {   
                health--;
            }
        }
    }

    void Update()
    {
        if (health < 1)
        {
            manager.UpdateScoreSmoothly(100);
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
