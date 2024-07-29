using System.Collections;
using System.Collections.Generic;
using LilLycanLord_Official;
using UnityEngine;

public class KeyCollection : MonoBehaviour
{
    [SerializeField]
    private int keysNeeded = 3;

    [SerializeField]
    private GameObject keyPrefab;

    [Header("Displays")]
    public int keys = 8;

    [SerializeField]
    private List<GameObject> boxes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // InitializeBoxes();
    }

    public void InitializeBoxes()
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

    public void AddBox(GameObject box)
    {
        if (box.tag == "Box")
            boxes.Add(box);
    }

    void CheckWin()
    {
        if (keys >= keysNeeded)
        {
            SceneTransitionManager.Instance.targetSceneName = "Good Ending";
            SceneTransitionManager.Instance.selectedTransitionName = "Crossfade";
            SceneTransitionManager.Instance.LoadSceneWithTransition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Box":
                if (boxes.Contains(other.gameObject) && keys < keysNeeded)
                {
                    if (KeyFound())
                    {
                        other.gameObject.GetComponent<BoxBehaviour>().SpawnObject(keyPrefab);
                        SimpleAudioManager.Instance.Play("Key Found");
                    }
                    else
                    {
                        SimpleAudioManager.Instance.Play("Box Open");
                    }
                    boxes.Remove(other.gameObject);
                    CheckWin();
                }
                break;
        }
    }

    bool KeyFound()
    {
        if (Random.Range(keysNeeded, keys + boxes.Count) == keysNeeded)
        {
            keys++;
            Debug.Log("Key found! : " + keys);
            return true;
        }
        else
        {
            Debug.Log("Not in here!");
            return false;
        }
    }
}
