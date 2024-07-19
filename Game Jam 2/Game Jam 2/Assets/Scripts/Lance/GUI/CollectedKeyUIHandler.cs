using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectedKeyUIHandler : MonoBehaviour
{
    [SerializeField] private KeyCollection keyCollection;
    [SerializeField] private Text collectedKeysText;

    private void Update()
    {
        collectedKeysText.text = string.Format("{0:00}", keyCollection.keys);
    }
}
