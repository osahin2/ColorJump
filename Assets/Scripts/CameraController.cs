using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject character;
    private Vector3 camFirstPos;
    private Vector3 camLastPos;

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player");

        camFirstPos = transform.position - character.transform.position;
    }

    private void FixedUpdate()
    {
        camLastPos = camFirstPos + character.transform.position;
        transform.position = Vector3.Lerp(transform.position, camLastPos, 5f * Time.deltaTime);
    }
}
