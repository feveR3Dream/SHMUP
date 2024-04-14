using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Management : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayOption playStatus;
    [SerializeField] Health playerDead;
    [SerializeField] BackToMenu restartStatus;
    [SerializeField] TextWriter textWriter;


    [Header("References")]
    /* Transforms */
    [SerializeField] Transform spawnPosition;
    [SerializeField] Transform scoreTextLerpPos;
    /* GameObjects */
    [SerializeField] GameObject phaseOneGroup;
    /* TextMeshProUGUI */
    [SerializeField] TextMeshProUGUI scoreHolder;
    [SerializeField] TextMeshProUGUI scoreText;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float slowDownSpeed;
    [SerializeField] float normalizeSpeed;
    [SerializeField] float desiredSlowSpeed;
    [SerializeField] float delayPhaseTime;
    [SerializeField] float delayShootTime;
    [SerializeField] float textAppearTime;
    [SerializeField] float textAppearSpeed;
    [SerializeField] float updateScoreSpeed;

    private float slowTime;
    private float normalizeTime;
    public int score;
    public int newUpdatedScore;
    /* Booleans */
    public bool canShoot;
    public bool canMove;
    public bool canTakeDamage;
    public bool canSpawn;
    private bool spawnAllowed = false;
    private bool scoreSpawn = false;

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
            StartCoroutine(ScoreTextSpawn());
            StartCoroutine(PhaseOneGroup());
            if (!scoreSpawn)
            {
                scoreSpawn = true;
                StartCoroutine(DelayScoreUpdate());
            }
        }
        deathSlowDownGame();
    }

    IEnumerator ScoreTextSpawn()
    {
        yield return new WaitForSeconds(0.01f);
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            float t = elapsedTime / textAppearSpeed;
            scoreText.transform.position = Vector2.Lerp(scoreText.transform.position, scoreTextLerpPos.position, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
        yield return new WaitForSeconds(textAppearTime);
        scoreHolder.gameObject.SetActive(true);
        UpdateScoreText();
    }

    IEnumerator UpdateScoreTextSmoothly(int newScore)
    {
        int oldScore = score;
        for (int i = oldScore; i <= newScore; i += 5)
        {
            score = i;          
            string stringScore = score.ToString("D14");
            scoreHolder.text = stringScore;
            textWriter.AddWriter(scoreHolder, scoreHolder.text, 0f, true);
            
            yield return new WaitForSeconds(0.1f / updateScoreSpeed);
        }
    }

    public void UpdateScoreSmoothly(int amount)
    {
        newUpdatedScore += amount;
        StartCoroutine(UpdateScoreTextSmoothly(newUpdatedScore));
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        string stringScore = score.ToString("D14");
        scoreHolder.text = stringScore; // Pay close attention to this part. [Linked with TextWrite script]
        textWriter.AddWriter(scoreHolder, scoreHolder.text, .05f, true);
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
