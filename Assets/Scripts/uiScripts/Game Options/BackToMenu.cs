using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [Header("References")]
    /* TextMeshPro */
    [SerializeField] TextMeshProUGUI ruh;
    [SerializeField] TextMeshProUGUI backToMenuText;
    /* Buttons */
    [SerializeField] Button B;


    [Header("Scripts")]
    private Health deadStatus;


    [Header("Values")]
    /* Booleans */
    public bool startAgain;
    private bool appeared;

    public void Start()
    {
        appeared = false;
        startAgain = false;
        ruh.gameObject.SetActive(false);
        backToMenuText.gameObject.SetActive(false);
        B.interactable = false;
        B.gameObject.SetActive(false);

        deadStatus = FindObjectOfType<Health>();
    }

    public void Update()
    {
        if (deadStatus.dead && !appeared)
        {
            appeared = true;
            ruh.gameObject.SetActive(true);
            backToMenuText.gameObject.SetActive(true);
            B.interactable = true;
            B.gameObject.SetActive(true);
        }
    }

    public void restartGame()
    {
        startAgain = true;
        appeared = false;
    }
}