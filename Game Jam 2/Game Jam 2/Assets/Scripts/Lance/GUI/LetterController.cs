using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterController : MonoBehaviour
{
    [SerializeField] private InstructionController instructionController;
    [SerializeField] private GameObject LetterUI;

    public void DisableLetterUI()
    {
        this.LetterUI.SetActive(false);
        this.instructionController.enablePageUpdateTrigger();
    }

}
