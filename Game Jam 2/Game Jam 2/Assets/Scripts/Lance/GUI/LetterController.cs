using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    [SerializeField] private InstructionController instructionController;
    [SerializeField] private GameObject LetterUI;
    [SerializeField] private GameObject EndButton;
    public void DisableLetterUI()
    {
        this.LetterUI.SetActive(false);
        this.instructionController.enablePageUpdateTrigger();
    }

    private void Start()
    {
        this.EndButton.SetActive(false);
    }

    private void Update()
    {
        if (instructionController.currentPage == instructionController.maxPage)
        {
            this.EndButton.SetActive(true);
        }
    }

}
