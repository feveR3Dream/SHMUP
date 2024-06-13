using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideStart : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayOption playStatus;
    [SerializeField] private SpriteRenderer self;


    [Header("Values")]
    /* Colors */
    private Color originalColorValue;
    private Color newColorValue;
    void Start()
    {
        originalColorValue = self.color;
        newColorValue = new Color(255/255f, 255/255f, 255/255f, 255/255f);
    }


    void Update()
    {
        if (!playStatus.started)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                self.color = newColorValue;
            }
            if (Input.GetKey(KeyCode.P))
            {
                self.color = newColorValue;
            }
            if (Input.GetKeyUp(KeyCode.P))
            {
                self.color = originalColorValue;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
