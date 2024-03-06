using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject evolutionOrb;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject evolveOrb = Instantiate(evolutionOrb, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
