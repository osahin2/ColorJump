using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject whiteBoy;
    private Vector3 camFirstPos;
    private Vector3 camLastPos;

    private void Awake()
    {
        whiteBoy = GameObject.FindGameObjectWithTag("Player");

        camFirstPos = transform.position - whiteBoy.transform.position;
    }

    private void FixedUpdate()
    {
        camLastPos = camFirstPos + whiteBoy.transform.position;
        transform.position = Vector3.Lerp(transform.position, camLastPos, 5f * Time.deltaTime);
    }
}
