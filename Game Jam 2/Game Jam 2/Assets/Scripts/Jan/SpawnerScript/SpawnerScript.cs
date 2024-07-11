using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    //a list of all the spawners
    public List<GameObject> spawners = new List<GameObject>();

    //holds the list of all the occupied boxes
    public List<GameObject> occupied = new List<GameObject>();
    

    //step one: add all spawners to a list (9 total) //done
    //step 2: choose where to spawn the 3 keys
    void Start()
    {
        foreach(Transform boxes in transform) {
            spawners.Add(boxes.gameObject);
        }


        //Debugging purposes
        Debug.Log("Boxes found: " + spawners.Count);


        //Will find a place to hide the key
        hideKeyInBox();
    }

    void hideKeyInBox()
    {

        while(occupied.Count < 3)
        {
            int boxNum = Random.Range(0,7);

            Debug.Log("random num: " + boxNum);
            
            if (occupied.Count <= 0) occupied.Add(spawners[boxNum].gameObject);   

            //if that box is part of the list
            foreach (GameObject occupiedBox in occupied)
            {
                // if (occupiedBox.gameObject.name == spawners[boxNum].gameObject.name ) break;
                //else, add to the list and add the key (will do setactive true instead if ever)
                if (occupiedBox.gameObject.name != spawners[boxNum].gameObject.name)
                {
                    occupied.Add(spawners[boxNum].gameObject);   
                    Debug.Log(boxNum);
                    
                } 
            }
        }
    }

}
