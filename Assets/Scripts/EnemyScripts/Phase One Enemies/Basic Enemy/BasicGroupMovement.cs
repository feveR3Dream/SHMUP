using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicGroupMovement : MonoBehaviour
{
    [SerializeField] float initialTravelDistance = 5f;
    //[SerializeField] float sideTravelDistances = 2f;
    [SerializeField] float lerpDistance = 2f;
    [SerializeField] float initialSpeed = 5f;
    [SerializeField] float sideSpeed = 1f;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] float initialDelayTime = 1f;
    [SerializeField] float movementDelayTime = 1f;
    [SerializeField] float radius = 1f;


    Vector2 downTargetPos;
    Vector2 lerpTargetPos;
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
            lerpTargetPos = (Vector2)transform.position - Vector2.up * lerpDistance;
        }
        
    }

    IEnumerator DelaySideMovement()
    {
        yield return new WaitForSeconds(movementDelayTime);
        
        //transform.position = Vector2.Lerp(transform.position, lerpTargetPos, lerpSpeed * Time.deltaTime);

        Vector3 movement = new Vector3(rightSide ? sideSpeed : -sideSpeed, 0f, 0f) * Time.deltaTime;
        transform.position += movement;
    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rightSide = !rightSide;
            Debug.Log("Is it working?");
        }
    }
    

    void CloseToWall()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                rightSide = !rightSide;
                Debug.Log("It worked");
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
