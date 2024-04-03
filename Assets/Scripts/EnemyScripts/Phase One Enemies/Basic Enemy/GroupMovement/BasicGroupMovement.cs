using System.Collections;
using UnityEngine;

public class BasicGroupMovement : MonoBehaviour
{
    [SerializeField] float initialTravelDistance = 5f;
    [SerializeField] float raycastDistance = 5f;
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float sideSpeed = 1f;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] float initialDelayTime = 1f;
    [SerializeField] float movementDelayTime = 1f;
    [SerializeField] float duration;
    [SerializeField] LayerMask rightLayerDetection;
    [SerializeField] LayerMask leftLayerDetection;
    [SerializeField] Transform lerpPosition;


    Vector2 downTargetPos;
    Vector2 downLerpPos;
    Vector2 initialLerpTargetPos;

    bool arrived;
    bool rightSide = false;
    bool upSide = false;
    bool allowMovement = false;

    [SerializeField] float time;

    void Start()
    {
        arrived = false;
        downTargetPos = (Vector2)transform.position - Vector2.up * initialTravelDistance;
        downLerpPos = (Vector2)lerpPosition.position - Vector2.up * initialTravelDistance;
    }

    void Update()
    {
        if (!arrived)
        {
            StartCoroutine(DelayInitialMovement());
        }
        else
        {
            StartCoroutine(DelaySideMovement());
            if (allowMovement)
            {
                SideMovement();
            }
        }
        Floating();
        CloseToWall();
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
        }

    }

    IEnumerator DelaySideMovement()
    {
        yield return new WaitForSeconds(movementDelayTime);

        allowMovement = true;
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

    void Floating()
    {
        if (time >= duration && allowMovement)
        {
            upSide = !upSide;
            time = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * raycastDistance);
        Gizmos.DrawRay(transform.position, -transform.right * raycastDistance);
    }
}
