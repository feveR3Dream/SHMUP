using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("References")]
    /* Animators */
    public Animator transition;


    [Header("Scripts")]
    private BackToMenu restart;
    

    [Header("Values")]
    /* Floats and Ints */
    public float transitionTime = 1f;

    void Start()
    {
        restart = FindObjectOfType<BackToMenu>();
    }

    void Update()
    {
        if (restart.startAgain)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
