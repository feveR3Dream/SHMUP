using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAppearDisappear : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI anyText;
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
