using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VultureHealth : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject bigDeathEffect;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] private int health = 50;
    /* Booleans */
    public bool isDead = false;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;
    private ScoreManager scoreManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
        if (enemyManager == null || scoreManager == null)
        {
            Debug.Log("Could not find EnemyPhaseManager or ScoreManager script");
        }

        IncreaseHealth();
    }

    void Update()
    {
        GameObject[] homingPlanes = GameObject.FindGameObjectsWithTag("HomingEnemy"); // Collect all of homing plane in the scene and put them in an array.

        if (health <= 0)
        {
            isDead = true;
            enemyManager.newWave = true;
            enemyManager.waveCounter++;
            enemyManager.loopWave++;
            enemyManager.canShoot = false;
            enemyManager.canTakeDamage = false;
            scoreManager.UpdateScoreSmoothly(5000);

            GameObject vultureDeathEffect = Instantiate(bigDeathEffect, transform.position, Quaternion.identity);
            Destroy(vultureDeathEffect, 2f);
            Destroy(body.gameObject);

            foreach(GameObject plane in homingPlanes) // If the vulture dies, all homing plane dies with it.
            {
                GameObject homingDeathEffect = Instantiate(deathEffect, plane.transform.position, Quaternion.identity);
                Destroy(homingDeathEffect, 2f);
                Destroy(plane.gameObject);

            }

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && enemyManager.canTakeDamage)
        {
            health--;
        }

        if (collision.gameObject.CompareTag("EnhancedBullet") && enemyManager.canTakeDamage)
        {
            health -= 2;
        }
    }

    void IncreaseHealth()
    {
        health = health + (5 * enemyManager.healthMultiplied);
    }
}
