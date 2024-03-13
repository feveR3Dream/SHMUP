using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeShootingPhaseOne : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform shootingPosition;
    public float bulletForce = 1f;
    public float timeBetweenShot = 2f;
    public float spinSpeed = 50f;
    
    void Start()
    {
        StartCoroutine(ShootBullets());
    }

    IEnumerator ShootBullets()
    {
        while (true)
        {
            GameObject bulletUp = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
            Rigidbody2D rbUp = bulletUp.GetComponent<Rigidbody2D>();
            rbUp.AddForce(Vector3.up * bulletForce, ForceMode2D.Impulse);

            GameObject bulletRight = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
            Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
            rbRight.AddForce(Vector3.right * bulletForce, ForceMode2D.Impulse);

            GameObject bulletLeft = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
            Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
            rbLeft.AddForce(Vector3.left * bulletForce, ForceMode2D.Impulse);

            GameObject bulletDown = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
            Rigidbody2D rbDown = bulletDown.GetComponent<Rigidbody2D>();
            rbDown.AddForce(Vector3.down * bulletForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(timeBetweenShot);
        }
    }  
}
