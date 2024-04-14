using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TextWriter : MonoBehaviour
{
    [Header("Values")]
    /* Floats and Ints */
    private int characterIndex;
    private float timePerCharacter;
    private float timer; // If a float value isn't assigned with anything, it's default value will be 0.0
    /* Strings */
    private string textToWrite;
    /* Booleans */
    private bool invisibleCharacters;


    [Header("References")]
    /* TextMeshPro */
    private TextMeshProUGUI uiText;


    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    private void Update()
    {
        if (uiText != null)
        {
            timer -= Time.deltaTime; // The value of timer being reduced every second.
            while (timer <= 0)
            {
                timer += timePerCharacter; // If timer hits 0 or lower, timer resets to the value of timePerCharacter.
                characterIndex++; // Increase character index after each timer reset.
                string text = textToWrite.Substring(0, characterIndex); // Print the previous character index before moving to a new one.

                ///////////////////////|
                if (invisibleCharacters)
                {
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>"; // [Need more time to understand and experiment]
                }
                ///////////////////////|

                uiText.text = text;

                if (characterIndex >= textToWrite.Length) // Avoid error message || Clamping the character index within the length of textToWrite.
                {
                    uiText = null;
                    return;
                }

            }

        }

    }
    
}
