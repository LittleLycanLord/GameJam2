using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform targetDestination;

    [SerializeField]
    GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleporting...");
        player.transform.position = targetDestination.position;
    }
}
