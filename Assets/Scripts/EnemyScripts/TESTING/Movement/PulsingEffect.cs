using System.Collections;
using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    public float minScale = 1f; 
    public float maxScale = 1.5f; 
    public float pulseSpeed = 1f; 

    void Start()
    {
        StartCoroutine(PulseEffect());     
    }

    IEnumerator PulseEffect()
    {
        while (true)
        {
            
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * pulseSpeed;
                transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
                yield return null;
            }

            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * pulseSpeed;
                transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * minScale, t);
                yield return null;
            }
            
        }
    }
}

