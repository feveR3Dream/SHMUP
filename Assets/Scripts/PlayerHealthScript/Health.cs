using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject spawnEffect;
    [SerializeField] GameObject player;

    private BackToMenu restart;
    public bool dead; 

    void Start()
    {
        dead = false;
        restart = FindObjectOfType<BackToMenu>();
    }

    void Update()
    {
        if (restart.startAgain)
        {
            dead = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            dead = true;
            GameObject deadEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(deadEffect, 2f);
            player.gameObject.SetActive(false);
        }

    }
}
