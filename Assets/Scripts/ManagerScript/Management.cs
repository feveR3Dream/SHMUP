using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Management : MonoBehaviour
{
    [SerializeField] PlayOption playStatus;
    [SerializeField] Health playerDead;
    [SerializeField] BackToMenu restartStatus;
    [SerializeField] Transform spawnPosition; 

    [SerializeField] GameObject phaseOneGroup;

    [SerializeField] TextMeshProUGUI scoreHolder;

    [SerializeField] TextWriter textWriter;

    public bool canShoot;
    public bool canMove;
    public bool canTakeDamage;
    public bool canSpawn;

    bool spawnAllowed = false;
    bool scoreSpawn = false;

    float slowTime;
    float normalizeTime;
    int score;

    [SerializeField] float slowDownSpeed;
    [SerializeField] float normalizeSpeed;
    [SerializeField] float desiredSlowSpeed;
    [SerializeField] float delayPhaseTime;
    [SerializeField] float delayShootTime;
    [SerializeField] float textAppear;

    void Start()
    {
        canShoot = false;
        canTakeDamage = false;
        canMove = false;
        scoreHolder.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playStatus.started && !spawnAllowed)
        {
            spawnAllowed = true;     
            StartCoroutine(PhaseOneGroup());
            if (!scoreSpawn)
            {
                scoreSpawn = true;
                StartCoroutine(DelayScoreUpdate());
            }
        }
        deathSlowDownGame();
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

    IEnumerator DelayScoreUpdate()
    {
        yield return new WaitForSeconds(textAppear);
        scoreHolder.gameObject.SetActive(true);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        string stringScore = score.ToString("D14");
        scoreHolder.text = stringScore; // Pay close attention to this part. [Linked with TextWrite script]
        textWriter.AddWriter(scoreHolder, scoreHolder.text, .05f);
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
