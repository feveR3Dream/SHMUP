using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCoroutine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject testingObject;

    [Header("Values")]
    [SerializeField] private float time;
    [SerializeField] private bool runProgram;

    private Coroutine timerCoroutine;

    private void Start()
    {
        runProgram = false;
    }

    void Update()
    {
        OnClick();
        if (runProgram)
        {
            time += Time.deltaTime;
        }
    }

    void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("StartCoroutine");
            // Start the coroutine and store the reference to it
            timerCoroutine = StartCoroutine(StartTimer()); // Storing StartCoroutine as a reference
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("StopCoroutine");
            // Stop the coroutine using the stored reference
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine); // Stopping the reference containing StartCoroutine (The only way to pause and reset a coroutine)
                timerCoroutine = null; // Reset the reference
            }
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(5f);
        runProgram = true;
    }
}

