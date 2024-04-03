using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    [SerializeField] PlayOption playStatus;
    [SerializeField] Health playerDead;
    [SerializeField] BackToMenu restartStatus;
    [SerializeField] Transform spawnPosition; 

    [SerializeField] GameObject phaseOneGroup;

    public bool canShoot;
    public bool canTakeDamage;
    public bool canSpawn;

    bool spawnAllowed = false;

    float slowTime;
    float normalizeTime;

    [SerializeField] float slowDownSpeed;
    [SerializeField] float normalizeSpeed;
    [SerializeField] float desiredSlowSpeed;
    [SerializeField] float delayPhaseTime;

    void Update()
    {
        if (playStatus.started && !spawnAllowed)
        {
            spawnAllowed = true; // Instant
            StartCoroutine(PhaseOneGroup());          
        }

        deathSlowDownGame();
    }

    IEnumerator PhaseOneGroup()
    {
        yield return new WaitForSeconds(delayPhaseTime);
        Instantiate(phaseOneGroup, spawnPosition.position, Quaternion.identity);
    }

    void PhaseOneBasicGroupSpawning()
    {

    }

    void deathSlowDownGame()
    {
        if (playerDead.dead && !restartStatus.startAgain)
        {
            slowTime = Mathf.MoveTowards(Time.timeScale, desiredSlowSpeed, slowDownSpeed * Time.deltaTime);
            Time.timeScale = slowTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            normalizeTime = Mathf.MoveTowards(Time.timeScale, 1f, normalizeSpeed * Time.deltaTime);
            Time.timeScale = normalizeTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}
