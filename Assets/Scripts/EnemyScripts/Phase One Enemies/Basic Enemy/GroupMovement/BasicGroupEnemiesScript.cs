using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicGroupEnemiesScript : MonoBehaviour
{
    [Header("Values")] // Will need to put away all of the [SerializeField] once everything is settled
    /* Floats and Ints */
    [SerializeField] float initialTravelDistance = 5f; /* Responsible for [1] */
    [SerializeField] float raycastDistance = 5f; /* Responsible for checking the distance of the enemy group from the wall */
    [SerializeField] float initialSpeed = 5f; /* Responsible for [1] */
    [SerializeField] float sideSpeed = 1f; /* Horizontal movement speed */ 
    [SerializeField] float lerpSpeed = 1f; /* Vertical movement speed */
    [SerializeField] float initialDelayTime = 1f; /* [1] How much time before entering the camera frame */ 
    [SerializeField] float duration; /* How much time before switching direction vertically */
    [SerializeField] float time; /* Timer, increase by a Time.deltaTime amount */
    public int BasicEnemiesCount = 0;


    [Header("References")]
    /* LayerMasks */
    [SerializeField] LayerMask rightLayerDetection;
    [SerializeField] LayerMask leftLayerDetection;
    /* Transforms */
    [SerializeField] Transform lerpPosition;
    [SerializeField] Transform enemiesContainer;
    /* GameObjects */
    [SerializeField] GameObject groupContainer;
    /* Vectors */
    Vector2 downTargetPos;
    Vector2 downLerpPos;
    Vector2 initialLerpTargetPos;


    [Header("Script")]
    private EnemyPhaseManager enemyManage;


    [Header("Booleans")]
    private bool arrived;
    private bool rightSide = false;
    private bool upSide = false;
    private bool allowMovement = false;
    public bool nextPhase = false;

    void Start()
    {
        enemyManage = FindObjectOfType<EnemyPhaseManager>();
        arrived = false;
        downTargetPos = (Vector2)transform.position - Vector2.up * initialTravelDistance;
        downLerpPos = (Vector2)lerpPosition.position - Vector2.up * initialTravelDistance;

        BasicEnemiesCount = enemiesContainer.childCount;

    }

    void Update()
    {
        Debug.Log(BasicEnemiesCount); // Testing
        if (!arrived)
        {
            StartCoroutine(DelayInitialMovement());
        }
        else
        {
            if (enemyManage.canMove && allowMovement)
            {
                SideMovement();
            }
            
        }
        FloatingEffect();
        CloseToWall();
        AllEnemyDied();
    }

    IEnumerator DelayInitialMovement()
    {
        yield return new WaitForSeconds(initialDelayTime);
        transform.position = Vector2.Lerp(transform.position, downTargetPos, initialSpeed * Time.deltaTime);
        lerpPosition.position = Vector2.Lerp(lerpPosition.position, downLerpPos, initialSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, downTargetPos) < 0.01f)
        {
            arrived = true;
            initialLerpTargetPos = transform.position;
            allowMovement = true;
        }
    }

    void SideMovement()
    {
        time += Time.deltaTime;

        float verticalValue = 0f;

        Vector3 verticalMovement;

        Vector3 horizontalMovement = new Vector3(rightSide ? sideSpeed : -sideSpeed, 0f, 0f) * Time.deltaTime;

        if (!upSide)
        {
            verticalValue = Mathf.Lerp(transform.position.y, lerpPosition.position.y, lerpSpeed * Time.deltaTime);
        }
        if (upSide)
        {
            verticalValue = Mathf.Lerp(transform.position.y, initialLerpTargetPos.y, lerpSpeed * Time.deltaTime);
        }

        verticalMovement = new Vector3(0f, verticalValue, 0f);

        transform.position += new Vector3(horizontalMovement.x, 0f, 0f);
        transform.position = new Vector3(transform.position.x, verticalMovement.y, 0f);
        lerpPosition.position += new Vector3(horizontalMovement.x, 0f, 0f);
    }

    void CloseToWall()
    {
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, transform.right, raycastDistance, rightLayerDetection);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, -transform.right, raycastDistance, leftLayerDetection);

        if (hitWallRight.collider != null || hitWallLeft.collider != null)
        {
            rightSide = !rightSide;
        }

    }

    void FloatingEffect()
    {
        if (time >= duration && allowMovement)
        {
            upSide = !upSide;
            time = 0;
        }
    }

    void AllEnemyDied() 
    {
        if (BasicEnemiesCount <= 0)
        {
            enemyManage.newWave = true;
            enemyManage.waveCounter++;
            enemyManage.canShoot = false;
            enemyManage.canTakeDamage = false;
            enemyManage.canMove = false;
            Destroy(groupContainer); 

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * raycastDistance);
        Gizmos.DrawRay(transform.position, -transform.right * raycastDistance);
    }
}
