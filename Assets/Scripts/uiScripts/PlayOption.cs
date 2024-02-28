using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayOption : MonoBehaviour
{
    public bool started; // Main Ui |enable|disable| variable.
    [SerializeField] Button playButton;
    [SerializeField] Health deadStatus;
    [SerializeField] GameObject starEffectOne;
    [SerializeField] GameObject starEffectTwo;
    [SerializeField] GameObject starEffectThree;

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
