using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAppearDisappear : MonoBehaviour
{
    [Header("References")]
    /* TextMeshPro */
    [SerializeField] TextMeshProUGUI anyText;


    [Header("Scripts")]
    [SerializeField] PlayOption playStatus;

    void Update()
    {
        if (playStatus.started)
        {
            anyText.gameObject.SetActive(false);
        } 
        else
        {
            anyText.gameObject.SetActive(true);
        }
    }
}
