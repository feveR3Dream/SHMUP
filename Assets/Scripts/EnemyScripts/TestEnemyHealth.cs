using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject evolutionOrb;

    public bool findOrb;

    void Start()
    {
        findOrb = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject evolveOrb = Instantiate(evolutionOrb, transform.position, Quaternion.identity);
        Instantiate(evolveOrb, transform.position, Quaternion.identity);
        findOrb = true;
        Destroy(gameObject);
    }
}
