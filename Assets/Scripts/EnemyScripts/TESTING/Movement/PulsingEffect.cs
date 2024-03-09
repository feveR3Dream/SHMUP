using System.Collections;
using UnityEngine;

public class PulsingEffect : MonoBehaviour
{
    public float minScale = 1f; // Minimum scale
    public float maxScale = 1.5f; // Maximum scale
    public float pulseSpeed = 1f; // Speed of pulsing

    void Start()
    {
        while (true)
        {
            StartCoroutine(PulseEffect());
        }
    }

    IEnumerator PulseEffect()
    {
        float t = 0f;

        // Scale up
        while (t < 1f)
        {
            t += Time.deltaTime * pulseSpeed;
            transform.localScale = Vector3.Lerp(Vector3.one * minScale, Vector3.one * maxScale, t);
            yield return null;
        }

        // Scale down
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * pulseSpeed;
            transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.one * minScale, t);
            yield return null;
        }
    }
}

