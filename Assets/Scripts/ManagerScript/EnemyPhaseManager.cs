using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EnemyPhaseManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayOption playStatus;
    [SerializeField] Health playerDead;
    [SerializeField] BackToMenu restartStatus;


    [Header("References")]
    /* Transforms */
    [SerializeField] Transform spawnPosition;
    /* GameObjects */
    [SerializeField] GameObject phaseOneGroup;
    /* TextMeshProUGUI */
    [SerializeField] TextMeshProUGUI phaseText;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float slowDownSpeed;
    [SerializeField] float normalizeSpeed;
    [SerializeField] float desiredSlowSpeed;
    [SerializeField] float delayPhaseTime;
    [SerializeField] float delayShootTime;
    private float slowTime;
    private float normalizeTime;
    public int waveCounter = 1;
    /* Booleans */
    public bool canShoot;
    public bool canMove;
    public bool canTakeDamage;
    public bool canSpawn;    
    public bool newWave;
    private bool firstPhase;   // Wave 1 - 5
    private bool secondPhase; // Wave 6 - 10
    private bool thirdPhase; // Wave 11 - 15
    private bool lastPhase; // Wave 16 - 20
    private bool phaseTextSpawn;
    private bool delayedSpawn;

    void Start()
    {
        canShoot = false;
        canTakeDamage = false;
        canMove = false;
        canSpawn = false;
        phaseText.gameObject.SetActive(false); 
        firstPhase = true; 
        secondPhase = false;
        thirdPhase = false; 
        lastPhase = false;
        phaseTextSpawn = false;
        delayedSpawn = false;
        newWave = true;
    }

    void Update()
    {   
        if (playStatus.started)
        {
            StartCoroutine(Delaying());

            if (delayedSpawn)
            {
                Wave1To5();
            }

        }
        deathSlowDownGame();   
    }

    IEnumerator Delaying()
    {
        yield return new WaitForSeconds(2f);
        delayedSpawn = true;
    }

    IEnumerator PhaseOneGroup()
    {
        yield return new WaitForSeconds(delayPhaseTime);
        Instantiate(phaseOneGroup, spawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(delayShootTime);
        canShoot = true;
        canTakeDamage = true;
        canMove = true;
    }

    IEnumerator SpawnPhaseText() 
    {
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(true);
        phaseText.text = "Wave: " + waveCounter;
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(true);
        phaseText.text = "Wave: " + waveCounter;
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(true);
        phaseText.text = "Wave: " + waveCounter;
        yield return new WaitForSeconds(0.25f);
        phaseText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        canSpawn = true;
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

    void PhaseTextSpawner() // Initiated after the game has been started.
    {
        if (playStatus.started && !phaseTextSpawn)
        {
            phaseTextSpawn = true;
            StartCoroutine(SpawnPhaseText());
        }
    } 

    void Wave1To5() // WAVE 1 - 5 || Initiated after spawning phase text.
    {
        if (firstPhase && newWave && waveCounter == 1)
        {
            if (!canSpawn)
            {
                PhaseTextSpawner();
            }

            if (canSpawn)
            {
                phaseTextSpawn = false;
                newWave = false;
                canSpawn = false;
                StartCoroutine(PhaseOneGroup());
            }

        }

        if (firstPhase && newWave && waveCounter == 2)
        {
            if (!canSpawn)
            {
                PhaseTextSpawner();
            }

        }
    }
}
