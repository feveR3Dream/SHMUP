using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject evolutionOrb;
    [SerializeField] int health = 20;
    void OnCollisionEnter2D(Collision2D collision)
    {
        health--;
    }

    void Update()
    {
        if (health < 1)
        {
            GameObject evolveOrb = Instantiate(evolutionOrb, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
