using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeEnemyHealth : MonoBehaviour
{
    [Header("References")]
    /* GameObjects */
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject container;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] int health = 30;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;
    private ScoreManager scoreManager;
    private AoeGroupEnemy aoeCounter;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        aoeCounter = FindObjectOfType<AoeGroupEnemy>();

        if (enemyManager == null || scoreManager == null || aoeCounter == null)
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnhancedBullet"))
        {
            if (enemyManager.canTakeDamage)
            {
                health-=2;
            }
        }
    }

    void Update()
    {
        if (health < 1)
        {
            aoeCounter.AoeEnemyCount--;
            scoreManager.UpdateScoreSmoothly(1000);
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(container.gameObject);
        }

    }

    void IncreaseHealth()
    {
        health = health + (5 * enemyManager.healthMultiplied); // Worked
        Debug.Log(health);
    }
}
