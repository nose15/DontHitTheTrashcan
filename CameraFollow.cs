using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform character;
    [SerializeField] private float yOffset, zOffset;

    void Update()
    {
        transform.position = new Vector3(0, character.position.y + yOffset, character.position.z - zOffset);
    }
}
