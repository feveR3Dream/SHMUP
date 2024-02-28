using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EvolutionScript : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distanceToPlayer; // This part is Serialized to see if the distance checking system works or not.
    [SerializeField] float lerpToPlayerSpeed = 10f;
    [SerializeField] float maxSizeX;
    [SerializeField] float maxSizeY;
    [SerializeField] float minSizeX = 0.33f;
    [SerializeField] float minSizeY = 0.33f;


    public bool stageOne;
    public bool stageTwo;

    void Start()
    {
        maxSizeX = transform.lossyScale.x;
        maxSizeY = transform.lossyScale.y;
        stageOne = true;
        stageTwo = false;
    }


    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 2f)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * lerpToPlayerSpeed);

            float targetSizeX = Mathf.Lerp(minSizeX, maxSizeX, distanceToPlayer / 3);
            float targetSizeY = Mathf.Lerp(minSizeY, maxSizeY, distanceToPlayer / 3);   
            transform.localScale = new Vector2(targetSizeX, targetSizeY);
        }
        if (distanceToPlayer < 0.5f)
        {
            stageOne = false;
            stageTwo = true;
            Destroy(gameObject);
        }
    }
}
