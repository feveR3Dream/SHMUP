using System.Collections;
using UnityEngine;

public class StartTheGameCameraEffect : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayOption playStatus;
    [SerializeField] Health deadStatus;


    [Header("Values")]
    /* Floats and Ints */
    private float initialOrthographicSize;
    [SerializeField] float targetOrthographicSize = 5f;
    [SerializeField] float zoomSpeed = 1f;

    void Start()
    {
        initialOrthographicSize = mainCamera.orthographicSize; // .orthographicSize is mainly use as a zoom in/out function of Unity || Initial position
    }

    void Update()
    {
        if (playStatus.started)
        {
            ZoomCamera(targetOrthographicSize);
        }
        if (deadStatus.dead)
        {
            ZoomCamera(initialOrthographicSize); 
        }
    }

    void ZoomCamera(float targetSize)
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }
}


