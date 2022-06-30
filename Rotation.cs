using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
