using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;

    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }
    void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime; // The value of timer being reduced every second.
            if (timer <= 0)
            {
                timer += timePerCharacter; // If timer hits 0 or lower, timer resets to the value of timePerCharacter.
                characterIndex++; // Increase character index after each timer reset.
                uiText.text = textToWrite.Substring(0, characterIndex); // Print the previous character index before moving to a new one.

                if (characterIndex >= textToWrite.Length) // Avoid error message || Clamping the character index within the length of textToWrite.
                {
                    uiText = null;
                    return;
                }

            }

        }
        
    }
}
