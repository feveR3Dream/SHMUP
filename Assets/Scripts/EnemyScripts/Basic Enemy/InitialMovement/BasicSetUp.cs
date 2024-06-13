using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSetUp : MonoBehaviour
{
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 180f);
    }
}
