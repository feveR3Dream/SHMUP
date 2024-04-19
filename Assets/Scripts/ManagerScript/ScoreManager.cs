using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float textAppearTime;
    [SerializeField] float textAppearSpeed;
    [SerializeField] float updateScoreSpeed;
    private float initialScoreSpawnSpeed = 0.01f;
    public int score; 
    public int newUpdatedScore;
    /* Booleans */
    public bool spawnAllowed = false;           
    private bool scoreSpawn = false;          
    private bool finalScoreSpawn = false;     


    [Header("References")]
    /* TextMeshProUGUI */
    [SerializeField] TextMeshProUGUI scoreHolder;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreAfterDeath;
    [SerializeField] TextMeshProUGUI scoreTextDeath;
    /* Transform */
    [SerializeField] Transform scoreTextLerpPos;


    [Header("Scripts")]
    [SerializeField] PlayOption playStatus;
    [SerializeField] TextWriter textWriter;
    [SerializeField] Health playerDead;
    [SerializeField] BackToMenu restartStatus;

    void Start()
    {
        scoreHolder.gameObject.SetActive(false);            
        scoreAfterDeath.gameObject.SetActive(false);        
        scoreTextDeath.gameObject.SetActive(false);      
    }

    void Update()
    {
        ScoreDisplay();
        deathShowScore();
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

    IEnumerator DelayScoreUpdate() // Initial score set up when the game begins.
    {
        yield return new WaitForSeconds(textAppearTime);
        scoreHolder.gameObject.SetActive(true);
        UpdateScoreText();
    }

    IEnumerator UpdateScoreTextSmoothly(int newScore) // Called within a basic enemy script. 
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

    public void UpdateScoreSmoothly(int amount) // Called within a basic enemy script.
    {
        newUpdatedScore += amount;
        StartCoroutine(UpdateScoreTextSmoothly(newUpdatedScore));
    }

    void UpdateScoreText() // Initial score set up when the game begins.
    {
        string stringScore = score.ToString("D14");
        scoreHolder.text = stringScore; // Pay close attention to this part. [Linked with TextWrite script]
        textWriter.AddWriter(scoreHolder, scoreHolder.text, initialScoreSpawnSpeed, true);
    }

    void UpdateScoreFinal() // Upon player's death, it will be set to active.
    {
        string stringScore = newUpdatedScore.ToString("D14");
        scoreAfterDeath.text = stringScore; // Pay close attention to this part. [Linked with TextWrite script]
        textWriter.AddWriter(scoreAfterDeath, scoreAfterDeath.text, initialScoreSpawnSpeed, true);
    }

    void ScoreDisplay() // Main function that display the score.
    {
        if (playStatus.started && !spawnAllowed)
        {
            spawnAllowed = true;
            StartCoroutine(ScoreTextSpawn()); 
            if (!scoreSpawn) 
            {
                scoreSpawn = true;
                StartCoroutine(DelayScoreUpdate());
            }
        }
    }
    void deathShowScore()
    {
        if (playerDead.dead && !restartStatus.startAgain && !finalScoreSpawn)
        {
            finalScoreSpawn = true;
            scoreHolder.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            scoreTextDeath.gameObject.SetActive(true);
            scoreAfterDeath.gameObject.SetActive(true);
            UpdateScoreFinal();
        }
    }
}
