using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootingPhaseOne : MonoBehaviour
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


    [Header("Scripts")]
    private EnemyPhaseManager shootAllow;

    void Start()
    {
        shootAllow = FindObjectOfType<EnemyPhaseManager>();
    }

    void Update()
    {
        if (canShoot && shootAllow.canShoot)
        {
            StartCoroutine(ShootFunction());
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
