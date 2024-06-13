using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeShooting : MonoBehaviour
{
    [Header("Values")]
    /* Floats and Ints */
    public float bulletForce = 1f;
    public float timeBetweenShot = 2f;
    public float spinSpeed = 50f;

    private bool canShoot = true;

    [SerializeField] float degreeAngleUp;
    [SerializeField] float degreeAngleUpRight;
    [SerializeField] float degreeAngleUpLeft;

    [SerializeField] float degreeAngleRight;
    [SerializeField] float degreeAngleRightUp;
    [SerializeField] float degreeAngleRightDown;

    [SerializeField] float degreeAngleLeft;
    [SerializeField] float degreeAngleLeftUp;
    [SerializeField] float degreeAngleLeftDown;

    [SerializeField] float degreeAngleDown;
    [SerializeField] float degreeAngleDownRight;
    [SerializeField] float degreeAngleDownLeft;


    [Header("References")]
    /* GameObjects */
    public GameObject enemyBullet;
    /* Transforms */
    public Transform shootingPosition;


    [Header("Scripts")]
    private EnemyPhaseManager enemyManage;


    void Start()
    {
        canShoot = true;

        GameObject game = GameObject.Find("GAME");
        if (game != null)
        {
            Transform manager = game.transform.Find("MANAGER");
            if (manager != null)
            {
                Transform enemyManager = manager.transform.Find("Enemy Phase Manager");

                if (enemyManager != null)
                {
                    enemyManage = enemyManager.GetComponent<EnemyPhaseManager>();
                    if (enemyManage == null)
                    {
                        Debug.Log("Could not find EnemyPhaseManager script");
                    }

                }
            }
        }

        IncreaseFireRate();

    }

    void IncreaseFireRate()
    {
        timeBetweenShot -= (0.2f * enemyManage.WaveAlterValue);
    }

    void Update() 
    {
        if (timeBetweenShot < 1f)
        {
            timeBetweenShot = 1f;
        }

        if (canShoot && enemyManage.canShoot)
        {
            StartCoroutine(ShootBullets());
        }


        if (degreeAngleUp < 360f) 
        {
            degreeAngleUp = degreeAngleUp + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleUp >= 360f) 
        {
            degreeAngleUp = 0;
        }

        if (degreeAngleUpRight < 375f) 
        {
            degreeAngleUpRight = degreeAngleUpRight + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleUpRight >= 375f) 
        {
            degreeAngleUpRight = 15f;
        }

        if (degreeAngleUpLeft < 345f) 
        {
            degreeAngleUpLeft = degreeAngleUpLeft + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleUpLeft >= 345f) 
        {
            degreeAngleUpLeft = -15f;
        }

        if (degreeAngleRight < 450f) 
        {
            degreeAngleRight = degreeAngleRight + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleRight >= 450f) 
        {
            degreeAngleRight = 90f; 
        }

        if (degreeAngleRightUp < 435f) 
        {
            degreeAngleRightUp = degreeAngleRightUp + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleRightUp >= 435f) 
        {
            degreeAngleRightUp = 75f;
        }

        if (degreeAngleRightDown < 465f) 
        {
            degreeAngleRightDown = degreeAngleRightDown + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleRightDown >= 465f) 
        {
            degreeAngleRightDown = 105f;
        }

        if (degreeAngleDown < 540f) 
        {
            degreeAngleDown = degreeAngleDown + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleDown >= 540f) 
        {
            degreeAngleDown = 180f;
        }

        if (degreeAngleDownRight < 525f) 
        {
            degreeAngleDownRight = degreeAngleDownRight + (Time.deltaTime * spinSpeed);

        }
        else if (degreeAngleDownRight >= 525f) 
        {
            degreeAngleDownRight = 165f;
        }

        if (degreeAngleDownLeft < 555f) 
        {
            degreeAngleDownLeft = degreeAngleDownLeft + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleDownLeft >= 555f) 
        {
            degreeAngleDownLeft = 195f;
        }

        if (degreeAngleLeft < 630f) 
        {
            degreeAngleLeft = degreeAngleLeft + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleLeft >= 630f) 
        {
            degreeAngleLeft = 270f;
        }

        if (degreeAngleLeftUp < 645f) 
        {
            degreeAngleLeftUp = degreeAngleLeftUp + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleLeftUp >= 645f) 
        {
            degreeAngleLeftUp = 285f;
        }

        if (degreeAngleLeftDown < 615f) 
        {
            degreeAngleLeftDown = degreeAngleLeftDown + (Time.deltaTime * spinSpeed);
        }
        else if (degreeAngleLeftDown >= 615f) 
        {
            degreeAngleLeftDown = 255f;
        }
    }

    IEnumerator ShootBullets()
    {
        canShoot = false;

        float xcoordinateUp = Mathf.Cos(degreeAngleUp * Mathf.PI/180) * bulletForce;
        float ycoordinateUp = Mathf.Sin(degreeAngleUp * Mathf.PI/180) * bulletForce;
            
        float xcoordinateUpRight = Mathf.Cos(degreeAngleUpRight * Mathf.PI/180) * bulletForce;
        float ycoordinateUpRight = Mathf.Sin(degreeAngleUpRight * Mathf.PI/180) * bulletForce;
            
        float xcoordinateUpLeft = Mathf.Cos(degreeAngleUpLeft * Mathf.PI/180) * bulletForce;
        float ycoordinateUpLeft = Mathf.Sin(degreeAngleUpLeft * Mathf.PI/180) * bulletForce;

        float xcoordinateRight = Mathf.Cos(degreeAngleRight * Mathf.PI/180) * bulletForce;
        float ycoordinateRight = Mathf.Sin(degreeAngleRight * Mathf.PI/180) * bulletForce;

        float xcoordinateRightUp = Mathf.Cos(degreeAngleRightUp * Mathf.PI/180) * bulletForce;
        float ycoordinateRightUp = Mathf.Sin(degreeAngleRightUp * Mathf.PI/180) * bulletForce;

        float xcoordinateRightDown = Mathf.Cos(degreeAngleRightDown * Mathf.PI/180) * bulletForce;
        float ycoordinateRightDown = Mathf.Sin(degreeAngleRightDown * Mathf.PI/180) * bulletForce;

        float xcoordinateLeft = Mathf.Cos(degreeAngleLeft * Mathf.PI/180) * bulletForce;
        float ycoordinateLeft = Mathf.Sin(degreeAngleLeft * Mathf.PI/180) * bulletForce;

        float xcoordinateLeftUp = Mathf.Cos(degreeAngleLeftUp * Mathf.PI/180) * bulletForce;
        float ycoordinateLeftUp = Mathf.Sin(degreeAngleLeftUp * Mathf.PI/180) * bulletForce;

        float xcoordinateLeftDown = Mathf.Cos(degreeAngleLeftDown * Mathf.PI/180) * bulletForce;
        float ycoordinateLeftDown = Mathf.Sin(degreeAngleLeftDown * Mathf.PI/180) * bulletForce;

        float xcoordinateDown = Mathf.Cos(degreeAngleDown * Mathf.PI/180) * bulletForce;
        float ycoordinateDown = Mathf.Sin(degreeAngleDown * Mathf.PI/180) * bulletForce;

        float xcoordinateDownRight = Mathf.Cos(degreeAngleDownRight * Mathf.PI/180) * bulletForce;
        float ycoordinateDownRight = Mathf.Sin(degreeAngleDownRight * Mathf.PI/180) * bulletForce;

        float xcoordinateDownLeft = Mathf.Cos(degreeAngleDownLeft * Mathf.PI/180) * bulletForce;
        float ycoordinateDownLeft = Mathf.Sin(degreeAngleDownLeft * Mathf.PI/180) * bulletForce;
            

            
        //////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject bulletUpMid = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbUpMid = bulletUpMid.GetComponent<Rigidbody2D>();
        rbUpMid.AddForce(new Vector2(ycoordinateUp, xcoordinateUp), ForceMode2D.Impulse);
            

        GameObject bulletUpRight = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbUpRight = bulletUpRight.GetComponent<Rigidbody2D>();
        rbUpRight.AddForce(new Vector2(ycoordinateUpRight, xcoordinateUpRight), ForceMode2D.Impulse);

            
        GameObject bulletUpLeft = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbUpLeft = bulletUpLeft.GetComponent<Rigidbody2D>();
        rbUpLeft.AddForce(new Vector2(ycoordinateUpLeft, xcoordinateUpLeft), ForceMode2D.Impulse);
        //////////////////////////////////////////////////////////////////////////////////////////

            
        //////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject bulletRight = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbRight = bulletRight.GetComponent<Rigidbody2D>();
        rbRight.AddForce(new Vector2(ycoordinateRight, xcoordinateRight), ForceMode2D.Impulse);

        GameObject bulletRightUp = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbRightUp = bulletRightUp.GetComponent<Rigidbody2D>();
        rbRightUp.AddForce(new Vector2(ycoordinateRightUp, xcoordinateRightUp), ForceMode2D.Impulse);

        GameObject bulletRightDown = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbRightDown = bulletRightDown.GetComponent<Rigidbody2D>();
        rbRightDown.AddForce(new Vector2(ycoordinateRightDown, xcoordinateRightDown), ForceMode2D.Impulse);
        ///////////////////////////////////////////////////////////////////////////////////////////////////
            
            
        /////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject bulletLeft = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbLeft = bulletLeft.GetComponent<Rigidbody2D>();
        rbLeft.AddForce(new Vector2(ycoordinateLeft, xcoordinateLeft), ForceMode2D.Impulse);

        GameObject bulletLeftUp = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbLeftUp = bulletLeftUp.GetComponent<Rigidbody2D>();
        rbLeftUp.AddForce(new Vector2(ycoordinateLeftUp, xcoordinateLeftUp), ForceMode2D.Impulse);

        GameObject bulletLeftDown = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbLeftDown = bulletLeftDown.GetComponent<Rigidbody2D>();
        rbLeftDown.AddForce(new Vector2(ycoordinateLeftDown, xcoordinateLeftDown), ForceMode2D.Impulse);
        ////////////////////////////////////////////////////////////////////////////////////////////////
            
            
        /////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject bulletDown = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbDown = bulletDown.GetComponent<Rigidbody2D>();
        rbDown.AddForce(new Vector2(ycoordinateDown, xcoordinateDown), ForceMode2D.Impulse);

        GameObject bulletDownRight = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbDownRight = bulletDownRight.GetComponent<Rigidbody2D>();
        rbDownRight.AddForce(new Vector2(ycoordinateDownRight, xcoordinateDownRight), ForceMode2D.Impulse);

        GameObject bulletDownLeft = Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
        Rigidbody2D rbDownLeft = bulletDownLeft.GetComponent<Rigidbody2D>();
        rbDownLeft.AddForce(new Vector2(ycoordinateDownLeft, xcoordinateDownLeft), ForceMode2D.Impulse);
        ////////////////////////////////////////////////////////////////////////////////////////////////
            
        yield return new WaitForSeconds(timeBetweenShot);

        canShoot = true;
    }  

}
