using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootingPhaseOne : MonoBehaviour
{
    [SerializeField] Transform shootDirection;
    [SerializeField] GameObject basicBullet;

    [SerializeField] float bulletForce = 5f;
    [SerializeField] float resetRandomShot = 0.75f;
    [SerializeField] int rngNumber = 10;

    bool canShoot = true;

    public GameObject fireEffect;

    void Update()
    {
        if (canShoot)
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
