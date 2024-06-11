using System.Collections;
using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float maxScale = 1.5f; 
    [SerializeField] private float pulseSpeed = 1f;
    private float lerpTime = 0f;
    private Vector2 originalSize;
    private Vector2 targetSize;


    void Start()
    { 
        originalSize = transform.localScale;
        targetSize = Vector2.one * maxScale;
    }


    private void Update()
    {
        ChangeSize();
    }


    void ChangeSize()
    {
        lerpTime += Time.deltaTime * pulseSpeed;
        transform.localScale = Vector2.Lerp(originalSize, targetSize, lerpTime);

        if (lerpTime > 5f)
        {
            Vector2 tempScale = originalSize;
            originalSize = targetSize;
            targetSize = tempScale;
            lerpTime = 0f;
        }
    }

}

