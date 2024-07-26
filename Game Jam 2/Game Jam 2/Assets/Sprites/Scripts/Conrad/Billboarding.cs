using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.youtube.com/watch?v=UcYfEfJW_mk
public class SpriteBillboarding : MonoBehaviour
{
    [SerializeField]
    bool freezeXZAxis = true;

    // Update is called once per frame
    void LateUpdate()
    {
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(
                0f,
                Camera.main.transform.rotation.eulerAngles.y,
                0f
            );
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
