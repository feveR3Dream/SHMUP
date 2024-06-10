using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnemyPhaseManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayOption playStatus;
    [SerializeField] private Health playerDead;
    [SerializeField] private BackToMenu restartStatus;


    [Header("References")]
    /* Transforms */
    [SerializeField] private Transform spawnPosition;
    /* GameObjects */
    [SerializeField] private GameObject basicEnemiesGroupContainer;
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

    private int textBlinkAmount = 0;
    public int waveCounter = 1;
    private int basicEnemiesCount = 0; // Default value of basic enemy


    /* Booleans */
    public bool canShoot = false; // Enemy firing ability
    public bool canMove = false; // Enemy movement ability
    public bool canTakeDamage = false; // Enemy vulnarable state
    public bool canSpawn = false; // Enemy spawn permission

    public bool newWave; // A boolean value that control the game waves.
    private bool waveTextSpawn = false;
    private bool delayedSpawn = false;

    void Start()
    {
        newWave = true;
        canShoot = false; 
        canMove = false; 
        canTakeDamage = false; 
        canSpawn = false; 
        waveTextSpawn = false;
        delayedSpawn = false;
        waveText.gameObject.SetActive(false);

    }

    void Update()
    {   
        if (basicEnemiesGroupContainer != null)
        {
            Transform enemiesContainer = basicEnemiesGroupContainer.transform.Find("BasicGroupEnemies"); // It does not matter if it is on scene or not
            if (enemiesContainer == null)
            {
                Debug.Log("Shit not running");
                return;
            }
            else
            {
                basicEnemiesCount = enemiesContainer.childCount;
            }
        }


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
        Instantiate(basicEnemiesGroupContainer, spawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(delayShootTime);
        canShoot = true;
        canTakeDamage = true;
        canMove = true;
    }

    IEnumerator SpawnWaveText() // Phase text flickering on and off until the next phase happens.
    {
        for (int i = 0; i < 5; i++) // Still, this might mess the game up, just save everything just in case
        {
            yield return new WaitForSeconds(0.25f);
            waveText.gameObject.SetActive(true);
            waveText.text = "Wave: " + waveCounter;
            yield return new WaitForSeconds(0.25f);
            waveText.gameObject.SetActive(false);
            textBlinkAmount++;
        }

        textBlinkAmount = 0;
        canSpawn = true;
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

    
    void EnemyWaveController() 
    {
        if (newWave)
        {
            if (!canSpawn)
            {
                StartCoroutine(WaveTextSpawner());
            }

            if (canSpawn)
            {
                waveTextSpawn = false;
                newWave = false;
                canSpawn = false;
                StartCoroutine(BasicEnemyGroup());
            }

        }

    }
    
    
}
