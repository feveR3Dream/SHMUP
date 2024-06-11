using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooting : MonoBehaviour
{
    [Header("References")]
    /* Transforms */
    [SerializeField] Transform shootDirection;
    /* GameObjects */
    [SerializeField] GameObject basicBullet;
    [SerializeField] GameObject fireEffect;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float bulletForce = 5f;
    [SerializeField] float resetRandomShot = 0.75f;
    [SerializeField] int rngNumber = 10;
    /* Booleans */ 
    bool canShoot = true;
    bool alterRNG = false;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManage;

    void Start()
    {
        GameObject game = GameObject.Find("GAME");
        if (game != null)
        {
            Transform manager = game.transform.Find("MANAGER");
            if (manager != null)
            {
                Transform enemyManager = manager.transform.Find("Enemy Phase Manager");

                if (enemyManager != null)
                {                
                    enemyManage = enemyManager.GetComponent<EnemyPhaseManager>();
                    if (enemyManage == null)
                    {
                        Debug.Log("Could not find EnemyPhaseManager script");
                    }

                } 
            }
        }
    }

    void Update()
    {
        ChangeRNG();

        if (canShoot && enemyManage.canShoot)
        {
            StartCoroutine(ShootFunction());
        }
    }

    void ChangeRNG()
    {
        if (!alterRNG)
        {
            alterRNG = true;
            rngNumber = rngNumber - enemyManage.WaveRNG;  
        }

        if (rngNumber < 10) // Cap maximum RNG amount.
        {
            rngNumber = 10;
        }

    }

    void ShootWithDelay()
    {
        GameObject effect = Instantiate(fireEffect, shootDirection.position, shootDirection.rotation);
        Destroy(effect, 0.5f);

        GameObject bullet = Instantiate(basicBullet, shootDirection.position, shootDirection.rotation * Quaternion.Euler(0f, 0f, 180f));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootDirection.up * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator ShootFunction()
    {
        canShoot = false;

        int randomNumber = Random.Range(1, rngNumber + 1);

        if (randomNumber ==  rngNumber)
        {
            ShootWithDelay();
        }

        yield return new WaitForSeconds(resetRandomShot);
        canShoot = true;
    }
}
