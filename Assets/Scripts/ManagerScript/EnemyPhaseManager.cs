using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnemyPhaseManager : MonoBehaviour  // MISSION: 1. FINISH WAVE RNG ||| 2. FINISH CLEANING YOUR SCRIPTS {Private and Public}
{
    [Header("Scripts")]
    [SerializeField] private PlayOption playStatus;
    [SerializeField] private Health playerDead;
    [SerializeField] private BackToMenu restartStatus;


    [Header("References")]
    /* Transforms */
    [SerializeField] private Transform basicEnemySpawnLocation;
    [SerializeField] private Transform aoeEnemySpawnLocationMiddle;
    [SerializeField] private Transform SOESpawnLocation;
    /* GameObjects */
    [SerializeField] private GameObject basicEnemiesGroupContainer;
    [SerializeField] private GameObject aoeEnemy;
    [SerializeField] private GameObject SOE;
    /* TextMeshProUGUI */
    [SerializeField] private TextMeshProUGUI waveText;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] private float slowDownSpeed;
    [SerializeField] private float normalizeSpeed;
    [SerializeField] private float desiredSlowSpeed;
    [SerializeField] private float delayPhaseTime;
    [SerializeField] private float delayShootTime;
    private float slowTime;
    private float normalizeTime;

    public int loopWave = 1;
    public int waveCounter = 1;
    public int healthMultiplied = -1;
    public int WaveRNG = 0; // In charge of modifying basic's bullet spawn RNG through each wave.


    /* Booleans */
    public bool canShoot = false; // Enemy firing ability
    public bool canMove = false; // Enemy movement ability
    public bool canTakeDamage = false; // Enemy vulnarable state
    public bool canSpawn = false; // Enemy spawn permission
    public bool canSpawnSOE = false;

    public bool newWave; // A boolean value that control the game waves.
    public bool waveTextSpawn = false;
    private bool delayedSpawn = false;
    private bool allowAlterRNG = false;

    void Start()
    {   // Why do I have to write it like this?
        newWave = true;
        canShoot = false; 
        canMove = false; 
        canTakeDamage = false; 
        canSpawn = false; 
        waveTextSpawn = false;
        delayedSpawn = false;
        canSpawnSOE = false;
        waveText.gameObject.SetActive(false);
        allowAlterRNG = false;
    }

    void Update()
    {   

        if (playStatus.started)
        {

            StartCoroutine(Delaying());

            if (delayedSpawn)
            {
                EnemyWaveController();
            }

        }
        DeathSlowDownGame();

    }

    IEnumerator Delaying()
    {
        yield return new WaitForSeconds(2f);
        delayedSpawn = true;          
    }

    IEnumerator BasicEnemyGroup()
    {
        yield return new WaitForSeconds(delayPhaseTime);
        Instantiate(basicEnemiesGroupContainer, basicEnemySpawnLocation.position, Quaternion.identity);
        yield return new WaitForSeconds(delayShootTime);
        canShoot = true;
        canTakeDamage = true;
        canMove = true;
    }

    IEnumerator AoeEnemy()
    {
        yield return new WaitForSeconds(delayPhaseTime);
        Instantiate(aoeEnemy, aoeEnemySpawnLocationMiddle.position, Quaternion.identity);
        yield return new WaitForSeconds(delayShootTime);
        canShoot = true;
        canTakeDamage = true;
    }

    IEnumerator SpawnWaveText() // Phase text flickering on and off until the next phase happens.
    {
        for (int i = 0; i < 3; i++) // Still, this might mess the game up, just save everything just in case
        {
            yield return new WaitForSeconds(0.25f);
            waveText.gameObject.SetActive(true);
            waveText.text = "Wave: " + waveCounter;
            yield return new WaitForSeconds(0.25f);
            waveText.gameObject.SetActive(false);
        }

        if (canSpawnSOE) // If it is spawning Star of Evolution, don't spawn enemies, and vice versa.
        {
            canSpawn = false;
        }
        else
        {        
            canSpawn = true;
        }

    }

    void DeathSlowDownGame()
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

    IEnumerator WaveTextSpawner() // Initiated after the game has been started.
    {
        if (playStatus.started && !waveTextSpawn)
        {
            waveTextSpawn = true;
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(SpawnWaveText());
        }
    } 

    IEnumerator SpawnSOE()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(SOE, SOESpawnLocation.position, Quaternion.identity);
    }

    
    void EnemyWaveController() 
    {
        if (newWave)
        {
            if (waveCounter % 2 == 0)
            {
                if (!allowAlterRNG)
                {
                    allowAlterRNG = true;
                    WaveRNG++; 
                }
            }
            else
            {
                allowAlterRNG = false;
            }

            switch (loopWave)
            {
                case 1:
                    if (!canSpawn)
                    {
                        StartCoroutine(WaveTextSpawner());

                    }

                    if (canSpawn)
                    {            
                        healthMultiplied++; // Increasing the amount of enemy's HP after each wave
                        waveTextSpawn = false;
                        newWave = false;
                        canSpawn = false;
                        StartCoroutine(BasicEnemyGroup());
                    }
                    break;

                case 2:
                    if (!canSpawn)
                    {
                        StartCoroutine(WaveTextSpawner());

                    }

                    if (canSpawn)
                    {
                        healthMultiplied++; 
                        waveTextSpawn = false;
                        newWave = false;
                        canSpawn = false;
                        StartCoroutine(AoeEnemy());
                    }
                    break;

                case 3:
                    if (!canSpawn)
                    {
                        StartCoroutine(WaveTextSpawner());

                    }

                    if (canSpawn)
                    {
                        healthMultiplied++; // Increasing the amount of enemy's HP after each wave
                        waveTextSpawn = false;
                        newWave = false;
                        canSpawn = false;
                        StartCoroutine(BasicEnemyGroup());
                    }
                    break;

                case 4:
                    if (!canSpawnSOE)
                    {
                        newWave = false;
                        canSpawnSOE = true;
                        StartCoroutine(WaveTextSpawner());
                        StartCoroutine(SpawnSOE());

                    }
                    break;
                case 5:
                    break;

                default: 
                    break;

            }
        
        }

    }
    
    
}
