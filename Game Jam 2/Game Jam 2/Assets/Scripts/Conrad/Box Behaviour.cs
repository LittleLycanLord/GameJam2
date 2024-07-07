using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float minimumOpeningSpeed;

    [SerializeField]
    [Range(1.0f, 2.0f)]
    private float maximumOpeningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                animator.SetFloat(
                    "OpeningSpeed",
                    Random.Range(minimumOpeningSpeed, maximumOpeningSpeed)
                );
                animator.SetTrigger("Open");
                break;
        }
    }
}
