using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootingPhaseOne : MonoBehaviour
{
    [SerializeField] Transform shootDirection;
    [SerializeField] GameObject basicBullet;

    [SerializeField] float bulletForce = 5f;
    [SerializeField] float timeBetweenShot = 0.75f;

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
        ShootWithDelay();
        yield return new WaitForSeconds(timeBetweenShot);
        canShoot = true;
    }
}
