using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootBullets : MonoBehaviour
{
    public Transform firePointFirstSecondEvolution; // Using Transform gives access to the desire direction/rotation/scale value we want to access/reference.
    public Transform firePointThirdEvolutionLeft;
    public Transform firePointThirdEvolutionRight;
    public GameObject firstEvolutionBullet;
    public GameObject secondEvolutionBullet;

    GameObject evolutionOrb;
    EvolutionScript evolutionScript;

    [SerializeField] PlayerFollowMouse PM;
    [SerializeField] PlayOption playStatus;

    [SerializeField] float bulletForce = 20f; // Bullet travelling speed
    [SerializeField] float timeBetweenShot = 0.5f; // Rate of fire
    [SerializeField] float maxDistance = 2f;

    [SerializeField] LayerMask detectionLayer;

    public bool evoStageOne;
    public bool evoStageTwo;
    public bool evoStageThree;
    public bool evoStageFour;
    public bool evoStageFive;

    float currentFireRate;
    bool canShoot = true;

    void Start()
    {
        evoStageOne = true;
        evoStageTwo = false;
        evoStageThree = false;
        evoStageFour = false;
        evoStageFive = false;
        currentFireRate = timeBetweenShot;
    }

    void Update()
    {
        evolutionOrb = GameObject.Find("Evolution"); 
        if (evolutionOrb == null)
        {
            Debug.Log("Could not find the orb"); // Should not do like this
        }
        else
        {
            evolutionScript = evolutionOrb.GetComponent<EvolutionScript>();    
            if (evolutionScript == null)
            {
                Debug.Log("Could not find the script"); // Haven't been checked
            }
        }

        if (playStatus.started)
        {
            StartCoroutine(PlayerShootFunction());
        }
        if (evolutionScript.stageOne) // Work
        {
            evoStageOne = true;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionScript.stageTwo) // Does not work when picked up
        {
            Debug.Log("Working");
            evoStageOne = false;
            evoStageTwo = true;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionScript.stageThree) // Does not work
        {
            evoStageOne = false;
            evoStageTwo= false;
            evoStageThree = true;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionScript.stageFour)
        {
            evoStageOne = false;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = true;
            evoStageFive = false;
        }
        if (evolutionScript.stageFive)
        {
            evoStageOne = false;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = true;
        }
    }

    IEnumerator StageOneDelayEachShot() // This function handles the delaying between each shots | Raycasting command would work in this function
    {
        canShoot = false;
        StageOneBullet();
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageOneBullet() // This function handles the Instantiation of bullets | STAGE ONE EVOLUTION
    {
        GameObject bullet = Instantiate(firstEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // We use GetComponent here because our firstEvolutionBullet is a PREFAB, thus, using GetComponent
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse); // AddForce is a normal practice to use when GameObject has a RigidBody component => Move stuff around.
    }

    IEnumerator StageTwoDelayEachShot() // This function handles the delaying between each shots | Raycasting command would work in this function
    {
        canShoot = false;
        StageTwoBullet();
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageTwoBullet() // | STAGE TWO EVOLUTION
    {
        Quaternion rotationRight = Quaternion.Euler(0f, 0f, 45f);
        Quaternion rotationLeft = Quaternion.Euler(0f, 0f, -45);

        GameObject bullet = Instantiate(firstEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        GameObject bulletRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -22.5f)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        Vector2 rightVector = rotationRight * Vector2.right;
        rbRight.AddForce((Vector2.up + rightVector).normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(firstEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, 22.5f)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        Vector2 leftVector = rotationLeft * Vector2.left;
        rbLeft.AddForce((Vector2.up + leftVector).normalized * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator StageThreeDelayEachShot() 
    {
        canShoot = false;
        StageThreeBullet();
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageThreeBullet() // Might need to check through this code
    {
        Quaternion rotationRight = Quaternion.Euler(0f, 0f, 45f);
        Quaternion rotationLeft = Quaternion.Euler(0f, 0f, -45);
        Quaternion rotationFarRight = Quaternion.Euler(0f, 0f, 90f);
        Quaternion rotationFarLeft = Quaternion.Euler(0f, 0f, -90f);

        GameObject bullet = Instantiate(firstEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        GameObject bulletRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -22.5f)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        Vector2 rightVector = rotationRight * Vector2.right;
        rbRight.AddForce((Vector2.up + rightVector).normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(secondEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, 22.5f)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        Vector2 leftVector = rotationLeft * Vector2.right;
        rbLeft.AddForce((Vector2.up + leftVector).normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bulletFarRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -45f)); // Shoot far right
        Rigidbody2D rbFarRight = bulletFarRight.GetComponent<Rigidbody2D>();
        Vector2 farRightVector = rotationFarRight * Vector2.right;
        rbFarRight.AddForce((Vector2.up + farRightVector).normalized * bulletForce, ForceMode2D.Impulse);

        GameObject bulletFarLeft = Instantiate(firstEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, 45f)); // Shoot far left
        Rigidbody2D rbFarLeft = bulletFarLeft.GetComponent<Rigidbody2D>();
        Vector2 farLeftVector = rotationFarLeft * Vector2.left;
        rbFarLeft.AddForce((Vector2.up + farLeftVector).normalized * bulletForce, ForceMode2D.Impulse);
    }

    IEnumerator PlayerShootFunction() // This function checks if the player has started the game or not | Started = Shoot
    {
        yield return new WaitForSeconds(PM.delayInitial);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxDistance, detectionLayer); // Raycast takes in original position, target direction, distance and layer
        if (hit.collider != null)
        {
            currentFireRate = timeBetweenShot / 2;
        }
        else
        {
            currentFireRate = timeBetweenShot;
        }

        if (canShoot)
        {
            if (evoStageOne)
            {
                StartCoroutine(StageOneDelayEachShot());
            }

            if (evoStageTwo)
            {
                StartCoroutine(StageTwoDelayEachShot());
            }
            if (evoStageThree)
            {
                StartCoroutine(StageThreeDelayEachShot());
            }
        }
    }
}
