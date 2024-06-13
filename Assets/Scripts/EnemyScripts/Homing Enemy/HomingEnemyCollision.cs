using UnityEngine;

public class HomingEnemyCollision : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject body; 


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] private int health = 4;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManager;
    private ScoreManager scoreManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyPhaseManager>();
        scoreManager = FindObjectOfType<ScoreManager>();

        if (enemyManager == null || scoreManager == null)
        {
            Debug.Log("Could not find EnemyPhaseManager or ScoreManager script");
        }

        IncreaseHealth();
    }


    private void Update()
    {
        if (health <= 0)
        {
            scoreManager.UpdateScoreSmoothly(250);
            GameObject effect = Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(body.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            Destroy(body.gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet") && enemyManager.canTakeDamage)
        {
            health--;
        }

        if (collision.gameObject.CompareTag("EnhancedBullet") && enemyManager.canTakeDamage)
        {
            health -= 2;
        }
    }

    void IncreaseHealth()
    {
        health = health + (2 * enemyManager.healthMultiplied);
    }
}
