using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTesting : MonoBehaviour
{
    [SerializeField] float speedRotation = 100f;
    [SerializeField] float speedRotationNew = 500f;
    GameObject anotherCube;

    void Start()
    {
        anotherCube = GameObject.FindWithTag("TESTING");
        if (anotherCube == null)
            Debug.Log("Could not find this MF");
        else 
            anotherCube = GetComponent<GameObject>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, speedRotation * Time.deltaTime);
        if (anotherCube != null)
            anotherCube.transform.Rotate(Vector3.forward, speedRotationNew * Time.deltaTime);
    }
}
