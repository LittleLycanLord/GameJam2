using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


//To be attached to Parent
public class BoxSpawner : MonoBehaviour
{
    public List<Transform> slots = new List<Transform>();

    //Reference Object (BOX)
    [SerializeField] private GameObject boxObject;
    [SerializeField] private int boxesToSpawn = 9;

    void Start()
    {

        //Get all the 12 children from the parent
        foreach(Transform child in transform) 
                slots.Add(child);

        
        //Finding 9 places to store boxes
        for(int i = 0; i < boxesToSpawn; i++)
        {
            Transform randomSlot = slots[Random.Range(0,slots.Count)].transform;
            Instantiate(boxObject,randomSlot);
            slots.Remove(randomSlot);
        }
    }

}