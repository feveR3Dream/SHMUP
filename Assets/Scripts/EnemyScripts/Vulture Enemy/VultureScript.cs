using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureScript : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    [SerializeField] private GameObject vulturePod; 
    [SerializeField] private GameObject homingEnemy; 
    [SerializeField] private GameObject cursedAmmunition; 
    [SerializeField] private GameObject fireEffect;
    private GameObject player;
    /* Transforms */
    [SerializeField] private Transform bulletPos;
    [SerializeField] private Transform homingSpawnPosOne;
    [SerializeField] private Transform homingSpawnPosTwo;
    /* RigidBody2Ds */
    private Rigidbody2D vultureRB;
    /* Coroutines */
    private Coroutine homingCoroutine;
    private Coroutine fireCoroutine;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] private float travelDistance;
    [SerializeField] private float travelSpeed;
    [SerializeField] private float firePow;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private float timeBetweenBurst;
    private int bulletCount = 0;
    /* Vector2s */
    private Vector2 playerLocation;
    private Vector2 targetLocation;
    /* Booleans */
    private bool canShoot = true;
    private bool spawnedHoming;


    [Header("Scripts")]
    [SerializeField] private VultureHealth aliveStatus;
    private EnemyPhaseManager enemyManager;

    void Start()
    {

        enemyManager = FindObjectOfType<EnemyPhaseManager>();

        if (enemyManager == null)
        {
            Debug.Log("Could not find EnemyPhaseManager script");
        }

        spawnedHoming = true;

        vultureRB = vulturePod.GetComponent<Rigidbody2D>();
        if (vultureRB == null)
        {
            Debug.Log("Can't find the vulture's Rigidbody2D component");
        }

        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.Log("Can't find player game object");
        }

        targetLocation = new Vector2(transform.position.x, transform.position.y - travelDistance);

        IncreaseBulletSpeed();

    }

    void IncreaseBulletSpeed()
    {
        firePow = firePow + (0.25f * enemyManager.WaveAlterValue);
    }

    void Update()
    {
        if (firePow > 7.5f)
        {
            firePow = 7.5f;
        }

        transform.position = Vector2.Lerp(transform.position, targetLocation, travelSpeed * Time.deltaTime); // Initial movement when first spawned 

        if (spawnedHoming && enemyManager.canShoot && !aliveStatus.isDead)
        {
            homingCoroutine = StartCoroutine(SpawnHomingEnemy());
        }

        SpawnHomingEnemy();
        LookAtPlayer();

        if (canShoot && enemyManager.canShoot && !aliveStatus.isDead)
        {
            fireCoroutine = StartCoroutine(VultureShooting());
        }

        if (aliveStatus.isDead)
        {
            StopCoroutine(homingCoroutine);
            StopCoroutine(fireCoroutine);
        }
    }


    IEnumerator SpawnHomingEnemy()
    {
        spawnedHoming = false;
        Instantiate(homingEnemy, homingSpawnPosOne.position, Quaternion.identity);
        Instantiate(homingEnemy, homingSpawnPosTwo.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        spawnedHoming = true;
    }

    void LookAtPlayer() // Constantly looking at the player
    {
        playerLocation = player.transform.position;

        Vector2 lookDir = playerLocation - (Vector2) vulturePod.transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        vultureRB.rotation = angle;
    }


    void Shooting()
    {
        Quaternion bulletRotation = bulletPos.rotation * Quaternion.Euler(0f, 0f, 180f);
        GameObject effect = Instantiate(fireEffect, bulletPos.position, bulletRotation);
        Destroy(effect, 0.5f);
        GameObject bullet = Instantiate(cursedAmmunition, bulletPos.position, bulletRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletPos.up * firePow, ForceMode2D.Impulse);
    }

    IEnumerator VultureShooting() // Vulture shooting mode is burst
    {
        canShoot = false;
        for (int i = 0; i < 5; i++)
        {
            bulletCount++;
            Shooting();
            yield return new WaitForSeconds(timeBetweenShot);
        }

        if (bulletCount == 5)
        { 
            yield return new WaitForSeconds(timeBetweenBurst);
            canShoot = true;
            bulletCount = 0;
        }

    }
}
