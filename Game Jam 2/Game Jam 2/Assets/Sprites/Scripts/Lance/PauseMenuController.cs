using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject InputManager;
    [SerializeField] private GameObject PauseCanvas;
    [SerializeField] private GameObject PauseMainMenu;
    [SerializeField] private GameObject HelpPanelCanvas;
    [SerializeField] private GameObject LeavePromptPanel;
    [SerializeField] private InstructionController instructionController;
    
    public void OpenPauseMenu()
    {
        PauseCanvas.SetActive(true);
        InputManager.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void ClosePauseMenu()
    {
        PauseCanvas.SetActive(false);
        InputManager.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void OpenHelpPanel()
    {
        HelpPanelCanvas.SetActive(true);
        PauseMainMenu.SetActive(false);
        instructionController.enablePageUpdateTrigger();
        
    }

    public void CloseHelpPanel()
    {
        if (this.instructionController.checkIfReachedMaxPage())
        {
            PauseMainMenu.SetActive(true);
            HelpPanelCanvas.SetActive(false);
            this.instructionController.returnToPauseMenu();
        }
      
    }

    public void OpenLeavePrompt()
    {
        LeavePromptPanel.SetActive(true);
        PauseMainMenu?.SetActive(false);    
    }

    public void OnClickLeaveNo()
    {
        PauseMainMenu.SetActive(true);
        LeavePromptPanel.SetActive(false);
    }

    private void Start()
    {
        PauseCanvas.SetActive(false);
        HelpPanelCanvas.SetActive(false);
        LeavePromptPanel.SetActive(false);
    }
}
