using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayOption : MonoBehaviour
{
    [Header("Values")]
    public bool started; // Main Ui |enable|disable| variable.


    [Header("References")]
    /* Buttons */
    [SerializeField] Button playButton;
    /* GameObjects */
    [SerializeField] GameObject starEffectOne;
    [SerializeField] GameObject starEffectTwo;
    [SerializeField] GameObject starEffectThree;


    [Header("Scripts")]
    [SerializeField] Health deadStatus;

    void Start()
    {
        started = false;
        playButton.interactable = true;
        playButton.gameObject.SetActive(true);
        starEffectOne.gameObject.SetActive(false);
        starEffectTwo.gameObject.SetActive(false);
        starEffectThree.gameObject.SetActive(false);
    }

    void Update()
    {
        if (deadStatus.dead)
        {
            started = false; // Will continue with this later
        }
        
    }
    public void StartTheGame()
    {
        started = true;
        playButton.interactable = false;
        playButton.gameObject.SetActive(false);
        starEffectOne.gameObject.SetActive(true);
        starEffectTwo.gameObject.SetActive(true);
        starEffectThree.gameObject.SetActive(true);
    }
}
