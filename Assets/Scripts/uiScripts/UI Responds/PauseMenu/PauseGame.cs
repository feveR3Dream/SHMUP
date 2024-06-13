using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    [SerializeField] private GameObject pauseMenu;


    [Header("Values")]
    /* Booleans */
    public bool MenuOpen = false;


    [Header("Scripts")]
    [SerializeField] private PlayOption playStatus;

    void Update()
    {
        if (playStatus.started)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenu.activeSelf)
                    Pause();
                else
                    ResumeGame();
            }
        }
    }

    public void Pause()
    {
        MenuOpen = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        MenuOpen = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}