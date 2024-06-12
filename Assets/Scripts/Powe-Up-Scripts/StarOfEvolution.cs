using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarOfEvolution : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject evolutionOrb;
    [SerializeField] private GameObject destroyEffect;
    private Quaternion spinValue;
    private Vector2 desireLocation;


    [Header("Values")]
    [SerializeField] private float moveDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float spinSpeed;
    private int health;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManage;

    void Start()
    {
        GameObject enemyManagerScriptGO = GameObject.Find("Enemy Phase Manager");
        if (enemyManagerScriptGO != null)
        {
            enemyManage = enemyManagerScriptGO.GetComponent<EnemyPhaseManager>();
            if (enemyManage == null)
            {
                Debug.Log("Could not find EnemyPhaseManager script");
            }
        }
        else
        {
            Debug.Log("Could not find Enemy Phase Manager Game Object in the scene");
        }


        spinValue = Quaternion.Euler(0f, 0f, 180f);
        desireLocation = new Vector2(transform.position.x, moveDistance);
        health = 50;
    }


    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, desireLocation, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, spinValue, spinSpeed * Time.deltaTime);

        StarDestroy();
    }


    void StarDestroy()
    {
        if (health <= 0)
        {
            enemyManage.canSpawnSOE = false;
            enemyManage.waveTextSpawn = false;
            enemyManage.newWave = true;
            enemyManage.waveCounter++;
            enemyManage.loopWave++;

            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Instantiate(evolutionOrb, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("EnhancedBullet"))
        {
            health--;
        }
    }

}
