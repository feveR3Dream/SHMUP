using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicGroupMovement : MonoBehaviour
{
    [SerializeField] float initialTravelDistance = 5f;
    //[SerializeField] float sideTravelDistances = 2f;
    [SerializeField] float lerpDistance = 2f;
    [SerializeField] float raycastDistance = 5f;
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float sideSpeed = 1f;
    //[SerializeField] float lerpSpeed = 1f;
    [SerializeField] float initialDelayTime = 1f;
    [SerializeField] float movementDelayTime = 1f;
    //[SerializeField] float radius = 1f;
    [SerializeField] float duration;

    [SerializeField] LayerMask rightLayerDetection;
    [SerializeField] LayerMask leftLayerDetection;

    Vector2 downTargetPos;
    Vector2 beforeLerpTargetPos;
    Vector2 lerpTargetPos;
    Vector2 targetPos;
    //Vector2 rightTargetPos;
    //Vector2 leftTargetPos;

    bool arrived;
    bool rightSide = false;

    void Start()
    {
        arrived = false;
        downTargetPos = (Vector2)transform.position - Vector2.up * initialTravelDistance;
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
        }
        CloseToWall();
    }

    IEnumerator DelayInitialMovement()
    {
        yield return new WaitForSeconds(initialDelayTime);
        transform.position = Vector2.Lerp(transform.position, downTargetPos, initialSpeed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, downTargetPos) < 0.01f)
        {
            arrived = true;
            beforeLerpTargetPos = transform.position;
            lerpTargetPos = (Vector2)transform.position - Vector2.up * lerpDistance;
        }
        
    }

    IEnumerator DelaySideMovement()
    {
        yield return new WaitForSeconds(movementDelayTime);

        /*
        float t = Mathf.PingPong(Time.time / duration, 1f);

        targetPos = Vector2.Lerp(beforeLerpTargetPos, lerpTargetPos, t);

        transform.position = targetPos;     
        */

        /*
        else if (Vector2.Distance(transform.position, lerpTargetPos) < 0.01)
        {
            transform.position = Vector2.Lerp(transform.position, beforeLerpTargetPos, lerpSpeed * Time.deltaTime);
            Debug.Log("Working");
        }
        */

        Vector3 movement = new Vector3(rightSide ? sideSpeed : -sideSpeed, 0f, 0f) * Time.deltaTime;
        transform.position += movement;
        
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rightSide = !rightSide;
            Debug.Log("Is it working?");
        }
    }
    */

    void CloseToWall()
    {
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, transform.right, raycastDistance, rightLayerDetection);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, -transform.right, raycastDistance, leftLayerDetection);

        if (hitWallRight.collider != null || hitWallLeft.collider != null)
        {
            rightSide = !rightSide;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay (transform.position, transform.right * raycastDistance);
        Gizmos.DrawRay (transform.position, -transform.right * raycastDistance);
    }
}
