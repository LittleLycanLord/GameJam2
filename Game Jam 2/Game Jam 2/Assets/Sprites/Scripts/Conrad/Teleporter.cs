using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform targetDestination;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleporting " + other.name + " to " + targetDestination.position + "...");

        other.transform.position = targetDestination.position;
    }
}
