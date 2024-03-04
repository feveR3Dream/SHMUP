using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] int evolutionCounter = 1;

    public bool stageOne;
    public bool stageTwo;
    public bool stageThree;
    public bool stageFour;
    public bool stageFive;

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
        stageOne = true;
        stageTwo = false;
        stageThree = false;
        stageFour = false;  
        stageFive = false;
    }


    void Update()
    {
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

            if (distanceToPlayer <= 0.01f)
            {
                evolutionCounter++;
                if (evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 2)
                {
                    Debug.Log("2nd Phase Working");
                    stageOne = false;
                    stageTwo = true;
                    stageThree = false;
                    stageFour = false;
                    stageFive = false;
                    Destroy(gameObject);
                }
                if (!evolutionStage.evoStageOne && evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 3)
                {
                    stageOne = false;
                    stageTwo = false;
                    stageThree = true;
                    stageFour = false;
                    stageFive = false;
                    Destroy(gameObject);
                }
                if (!evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 4)
                {
                    stageOne = false;
                    stageTwo = false;
                    stageThree = false;
                    stageFour = true;
                    stageFive = false;
                    Destroy(gameObject);
                }
                if (!evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 5)
                {
                    stageOne = false;
                    stageTwo = false;
                    stageThree = false;
                    stageFour = false;
                    stageFive = true;
                    Destroy(gameObject);
                }
            }
            
        }
        
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            evolutionCounter++;
            if (evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 2)
            {
                stageOne = false;
                stageTwo = true;
                stageThree = false;
                stageFour = false;
                stageFive = false;
                Destroy(gameObject);
            }
            if (!evolutionStage.evoStageOne && evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 3)
            {
                stageOne = false;
                stageTwo = false;
                stageThree = true;
                stageFour = false;
                stageFive = false;
                Destroy(gameObject);
            }
            if (!evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && evolutionStage.evoStageThree && !evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 4)
            {
                stageOne = false;
                stageTwo = false;
                stageThree = false;
                stageFour = true;
                stageFive = false;
                Destroy(gameObject);
            }
            if (!evolutionStage.evoStageOne && !evolutionStage.evoStageTwo && !evolutionStage.evoStageThree && evolutionStage.evoStageFour && !evolutionStage.evoStageFive && evolutionCounter == 5)
            {
                stageOne = false;
                stageTwo = false;
                stageThree = false;
                stageFour = false;
                stageFive = true;
                Destroy(gameObject);
            }
        }
    }
    */
}
