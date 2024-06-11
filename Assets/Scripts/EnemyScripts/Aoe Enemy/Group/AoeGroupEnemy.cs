using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeGroupEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform aoeContainer;


    [Header("Values")]
    public int AoeEnemyCount;

    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        if (enemyManager == null)
        {
            Debug.Log("Could not find EnemyPhaseManager script");
        }

        AoeEnemyCount = aoeContainer.childCount;
    }

    void Update()
    {
        AllEnemyDied();
    }

    void AllEnemyDied()
    {
        if (AoeEnemyCount <= 0)
        {
            enemyManager.newWave = true;
            enemyManager.waveCounter++;
            enemyManager.loopWave++;
            enemyManager.canShoot = false;
            enemyManager.canTakeDamage = false;
            Destroy(aoeContainer);

        }
    }
}
