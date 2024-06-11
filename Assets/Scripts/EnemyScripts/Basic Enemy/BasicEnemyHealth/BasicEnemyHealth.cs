using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    [Header("References")]
    /* GameObjects */
    [SerializeField] GameObject deathEffect;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] int health = 20;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;
    private ScoreManager scoreManager;
    private BasicGroupEnemiesScript enemyCounter;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        enemyCounter = FindObjectOfType<BasicGroupEnemiesScript>();

        if (enemyManager == null || scoreManager == null || enemyCounter == null)
        {
            Debug.Log("Where the hell are the scripts?");
        }

        IncreaseHealth();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (enemyManager.canTakeDamage)
            {   
                health--;
            }
        }
    }

    void Update()
    {


        if (health < 1)
        {
            scoreManager.UpdateScoreSmoothly(100);
            enemyCounter.BasicEnemiesCount--;
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }

    }

    void IncreaseHealth()
    {
        health = health + (2 * enemyManager.healthMultiplied); // Worked
    }
}
