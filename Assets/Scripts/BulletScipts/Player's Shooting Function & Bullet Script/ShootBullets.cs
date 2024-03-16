using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootBullets : MonoBehaviour
{
    public Transform firePointFirstSecondEvolution; 
    public Transform firePointThirdEvolutionLeft;
    public Transform firePointThirdEvolutionRight;
    public Transform firePointThirdEvolutionFarLeft;
    public Transform firePointThirdEvolutionFarRight;
    public GameObject firstEvolutionBullet;
    public GameObject secondEvolutionBullet;

    [SerializeField] PlayerKeyboardMovement PKM;
    [SerializeField] PlayOption playStatus;

    [SerializeField] float bulletForce = 20f; // Bullet travelling speed
    [SerializeField] float timeBetweenShot = 0.5f; // Rate of fire
    [SerializeField] float maxDistance = 2f; // RayCasting distance (from the player ---> enemy)
    [SerializeField] float degreeAngle; // Angle of the inner bullet spread (the 2nd bullet instantiation position from the middle bullet instantiation position [Both left and right]) 
    [SerializeField] float minDegreeAngle = 5f;
    [SerializeField] float maxDegreeAngle = 15f;
    [SerializeField] float outerDegreeAngle; // Angle of the outer bullet spread (the 3rd bullet instantiation position from the middle bullet instantiation position [Both left and right])
    [SerializeField] float minOuterDegreeAngle = 15f;
    [SerializeField] float maxOuterDegreeAngle = 30f;
    [SerializeField] int evolutionCounter = 1;
    [SerializeField] float duration = 1f; // Duration of bullet spreading in and out (measure in unit of second)

    [SerializeField] LayerMask detectionLayer;
    [SerializeField] LayerMask detectionLayerSecond;

    public bool evoStageOne;
    public bool evoStageTwo;
    public bool evoStageThree;
    public bool evoStageFour;
    public bool evoStageFive;

    float currentFireRate;
    bool canShoot = true;
    bool increasing;
    bool outerIncreasing;

    void Start()
    {
        evoStageOne = true;
        evoStageTwo = false;
        evoStageThree = false;
        evoStageFour = false;
        evoStageFive = false;
        currentFireRate = timeBetweenShot;

        degreeAngle = minDegreeAngle;
        outerDegreeAngle = minOuterDegreeAngle;
    }

    void Update()
    {
        /////////////////////////////////////////////////////////////////////////////////////////
        float t = Mathf.PingPong(Time.time / duration, 1f); // MUST REMEMBER //               //
        degreeAngle = Mathf.Lerp(minDegreeAngle, maxDegreeAngle, t);                         //
        if (degreeAngle >= maxDegreeAngle)                                                  //
        {                                                                                  //
            increasing = false;                                                           //
        }                                                                                //
        else if (degreeAngle <= minDegreeAngle)                                         //
        {                                                                              //
            increasing = true;                                                        //
        }                                                                            //   Bullet Spread
                                                                                    //
        if (increasing)                                                            //
        {                                                                         //
            degreeAngle = Mathf.Lerp(maxDegreeAngle, minDegreeAngle, t);         //
        }                                                                       //
                                                                               //
        if (!increasing)                                                      //    
        {                                                                    //
            degreeAngle = Mathf.Lerp(minDegreeAngle, maxDegreeAngle, t);    //
        }////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        outerDegreeAngle = Mathf.Lerp(minDegreeAngle, maxDegreeAngle, t);                                  //
        if (outerDegreeAngle >= maxOuterDegreeAngle)                                                      //
        {                                                                                                //
            outerIncreasing = false;                                                                    //
        }                                                                                              //
        else if (outerDegreeAngle <= minOuterDegreeAngle)                                             //
        {                                                                                            //
            outerIncreasing = true;                                                                 //
        }                                                                                          //
                                                                                                  //
        if (outerIncreasing)                                                                     //   Outer Bullet Spread
        {                                                                                       //
            outerDegreeAngle = Mathf.Lerp(maxOuterDegreeAngle, minOuterDegreeAngle, t);        //
        }                                                                                     //
                                                                                             //
        if (!outerIncreasing)                                                               //
        {                                                                                  //
            outerDegreeAngle = Mathf.Lerp(minOuterDegreeAngle, maxOuterDegreeAngle, t);   //
        }                                                                                //
        //////////////////////////////////////////////////////////////////////////////////

        if (playStatus.started)
        {
            StartCoroutine(PlayerShootFunction());
        }
        if (evolutionCounter == 1) // Work
        {
            evoStageOne = true;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionCounter == 2) // Work
        {
            evoStageOne = false;
            evoStageTwo = true;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionCounter == 3) // Work
        {
            evoStageOne = false;
            evoStageTwo= false;
            evoStageThree = true;
            evoStageFour = false;
            evoStageFive = false;
        }
        if (evolutionCounter == 4) // Work
        {
            evoStageOne = false;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = true;
            evoStageFive = false;
        }
        if (evolutionCounter == 5) // Work
        {
            evoStageOne = false;
            evoStageTwo = false;
            evoStageThree = false;
            evoStageFour = false;
            evoStageFive = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EvolutionOrb") && collision.gameObject.layer == LayerMask.NameToLayer("EvolutionOrb"))
        {
            evolutionCounter++;
        }
    }

    IEnumerator StageOneDelayEachShot() // This function handles the delaying between each shots 
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
    IEnumerator StageTwoDelayEachShot() // This function handles the delaying between each shots 
    {
        canShoot = false;
        StageTwoBullet(bulletForce, degreeAngle);
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageTwoBullet(float force, float angle) // | STAGE TWO EVOLUTION
    {
        GameObject bullet = Instantiate(firstEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force; // Cosine returns X component 
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force; // Sine returns Y component

        GameObject bulletRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -degreeAngle)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.AddForce(new Vector2(ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(firstEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, degreeAngle)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.AddForce(new Vector2(-ycomponent, xcomponent), ForceMode2D.Impulse); // rb.AddForce(Vector3.up * bulletForce, ForceMode2D.Impulse)
    }

    IEnumerator StageThreeDelayEachShot() 
    {
        canShoot = false;
        StageThreeBullet(bulletForce, degreeAngle, outerDegreeAngle);
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageThreeBullet(float force, float angle, float outerAngle) // | STAGE THREE EVOLUTION
    {
        GameObject bullet = Instantiate(secondEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force; 
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;
        float outerxcomponent = Mathf.Cos(outerAngle * Mathf.PI / 180) * force;
        float outerycomponent = Mathf.Sin(outerAngle * Mathf.PI / 180) * force;

        GameObject bulletRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -degreeAngle)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.AddForce(new Vector2(ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(firstEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, degreeAngle)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.AddForce(new Vector2(-ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletFarRight = Instantiate(firstEvolutionBullet, firePointThirdEvolutionFarRight.position, Quaternion.Euler(0f, 0f, -outerAngle)); // Shoot right 
        Rigidbody2D rbFarRight = bulletFarRight.GetComponent<Rigidbody2D>();
        rbFarRight.AddForce(new Vector2(outerycomponent, outerxcomponent), ForceMode2D.Impulse);

        GameObject bulletFarLeft = Instantiate(firstEvolutionBullet, firePointThirdEvolutionFarLeft.position, Quaternion.Euler(0f, 0f, outerAngle)); // Shoot left
        Rigidbody2D rbFarLeft = bulletFarLeft.GetComponent<Rigidbody2D>();
        rbFarLeft.AddForce(new Vector2(-outerycomponent, outerxcomponent), ForceMode2D.Impulse);
    }

    IEnumerator StageFourDelayEachShot() // This function handles the delaying between each shots 
    {
        canShoot = false;
        StageFourBullet(bulletForce, degreeAngle);
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageFourBullet(float force, float angle) // | STAGE FOUR EVOLUTION
    {
        GameObject bullet = Instantiate(secondEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force; // Cosine returns X component
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force; // Sine returns Y component

        GameObject bulletRight = Instantiate(secondEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -degreeAngle)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.AddForce(new Vector2(ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(secondEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, degreeAngle)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.AddForce(new Vector2(-ycomponent, xcomponent), ForceMode2D.Impulse);
    }

    IEnumerator StageFiveDelayEachShot() // This function handles the delaying between each shots 
    {
        canShoot = false;
        StageFiveBullet(bulletForce, degreeAngle, outerDegreeAngle);
        yield return new WaitForSeconds(currentFireRate);
        canShoot = true;
    }

    void StageFiveBullet(float force, float angle, float outerAngle) // | STAGE FIVE EVOLUTION
    {
        GameObject bullet = Instantiate(secondEvolutionBullet, firePointFirstSecondEvolution.position, Quaternion.identity); // Shoot straight
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);

        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;
        float outerxcomponent = Mathf.Cos(outerAngle * Mathf.PI / 180) * force;
        float outerycomponent = Mathf.Sin(outerAngle * Mathf.PI / 180) * force;

        GameObject bulletRight = Instantiate(secondEvolutionBullet, firePointThirdEvolutionRight.position, Quaternion.Euler(0f, 0f, -degreeAngle)); // Shoot right
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.AddForce(new Vector2(ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletLeft = Instantiate(secondEvolutionBullet, firePointThirdEvolutionLeft.position, Quaternion.Euler(0f, 0f, degreeAngle)); // Shoot left
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.AddForce(new Vector2(-ycomponent, xcomponent), ForceMode2D.Impulse);

        GameObject bulletFarRight = Instantiate(secondEvolutionBullet, firePointThirdEvolutionFarRight.position, Quaternion.Euler(0f, 0f, -outerAngle)); // Shoot right 
        Rigidbody2D rbFarRight = bulletFarRight.GetComponent<Rigidbody2D>();
        rbFarRight.AddForce(new Vector2(outerycomponent, outerxcomponent), ForceMode2D.Impulse);

        GameObject bulletFarLeft = Instantiate(secondEvolutionBullet, firePointThirdEvolutionFarLeft.position, Quaternion.Euler(0f, 0f, outerAngle)); // Shoot left
        Rigidbody2D rbFarLeft = bulletFarLeft.GetComponent<Rigidbody2D>();
        rbFarLeft.AddForce(new Vector2(-outerycomponent, outerxcomponent), ForceMode2D.Impulse);
    }

    IEnumerator PlayerShootFunction() // This function checks if the player has started the game or not | Started = Shoot
    {
        yield return new WaitForSeconds(PKM.delayInitial);
        RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, transform.up, maxDistance, detectionLayer); // Raycast takes in original position, target direction, distance and layer
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, transform.up, maxDistance, detectionLayerSecond);
        if (hitObstacle.collider != null || hitEnemy.collider != null)
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
            if (evoStageFour)
            {
                StartCoroutine(StageFourDelayEachShot());
            }
            if (evoStageFive)
            {
                StartCoroutine(StageFiveDelayEachShot());
            }
        }
    }
}
