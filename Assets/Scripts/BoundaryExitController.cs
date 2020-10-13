using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryExitController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
