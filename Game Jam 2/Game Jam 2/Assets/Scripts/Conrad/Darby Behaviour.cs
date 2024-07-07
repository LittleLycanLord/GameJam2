using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarbyBehaviour : MonoBehaviour
{
    [SerializeField]
    private int keysNeeded = 3;

    [Header("Displays")]
    [SerializeField]
    private int keys = 0;

    [SerializeField]
    private List<GameObject> boxes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject thing in things)
        {
            boxes.Add(thing);
        }
        if (keysNeeded > boxes.Count)
        {
            Debug.LogError("There are lesser boxes than keys.");
        }
    }

    // Update is called once per frame
    void CheckWin()
    {
        if (keys >= keysNeeded)
        {
            Debug.Log("WIN");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Box":
                if (boxes.Contains(other.gameObject) && keys < keysNeeded)
                {
                    KeyFound();
                    boxes.Remove(other.gameObject);
                    CheckWin();
                }
                break;
        }
    }

    void KeyFound()
    {
        if (Random.Range(keysNeeded, keys + boxes.Count) == keysNeeded)
        {
            keys++;
            Debug.Log("Key found! : " + keys);
        }
        else
        {
            Debug.Log("Not in here!");
        }
    }
}
