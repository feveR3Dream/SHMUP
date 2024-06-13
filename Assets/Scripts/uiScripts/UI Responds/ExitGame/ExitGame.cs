using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    [Header("References")]
    /* Game Objects */
    [SerializeField] private GameObject exitButton;


    [Header("Scripts")]
    [SerializeField] private PlayOption playStatus;


    private void Update()
    {
        if (playStatus.started)
        {
            exitButton.SetActive(false);
        }
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

}
