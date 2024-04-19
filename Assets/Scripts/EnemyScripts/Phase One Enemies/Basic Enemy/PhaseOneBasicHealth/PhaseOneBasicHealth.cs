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


    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;
    private ScoreManager scoreManager;
    private BasicGroupMovement enemyCounter;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        enemyCounter = FindObjectOfType<BasicGroupMovement>();
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
            enemyCounter.basicEnemiesCount--;
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
