using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EvolutionScript : MonoBehaviour
{
    [SerializeField] float distanceToPlayer; // This part is Serialized to see if the distance checking system works or not.
    [SerializeField] float lerpToPlayerSpeed = 10f;
    [SerializeField] float maxSizeX;
    [SerializeField] float maxSizeY;
    [SerializeField] float minSizeX = 0.33f;
    [SerializeField] float minSizeY = 0.33f;
    [SerializeField] float moveSpeed = 1.5f;

    GameObject findPlayer;
    ShootBullets evolutionStage;

    void Start()
    {
        findPlayer = GameObject.Find("Player-Plane"); 
        if (findPlayer == null)
        {
            Debug.Log("Can't find the player's plane.");
        } 
        else
        {
            evolutionStage = findPlayer.GetComponent<ShootBullets>();
            if (evolutionStage == null)
            {
                Debug.Log("Can't find the script.");
            }
        }

        maxSizeX = transform.lossyScale.x;
        maxSizeY = transform.lossyScale.y;
    }


    void Update()
    {
        Vector2 moveDown = (Vector2)transform.position + new Vector2(0f, -moveSpeed * Time.deltaTime);
        transform.position = moveDown;

        if (findPlayer != null && evolutionStage != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, findPlayer.transform.position);
            if (distanceToPlayer < 2f) 
            {
                transform.position = Vector2.Lerp(transform.position, findPlayer.transform.position, Time.deltaTime * lerpToPlayerSpeed);

                float targetSizeX = Mathf.Lerp(minSizeX, maxSizeX, distanceToPlayer / 3);
                float targetSizeY = Mathf.Lerp(minSizeY, maxSizeY, distanceToPlayer / 3);

                transform.localScale = new Vector2(targetSizeX, targetSizeY);
            }      
        } 
        Destroy(gameObject, 15f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    
}
