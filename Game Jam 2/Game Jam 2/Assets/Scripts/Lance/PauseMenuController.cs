using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject InputManager;
    [SerializeField] private GameObject PauseMenuCanvas;
    public void OpenPauseMenu()
    {
        PauseMenuCanvas.SetActive(true);
        InputManager.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void ClosePauseMenu()
    {
        PauseMenuCanvas.SetActive(false);
        InputManager.SetActive(true);
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        PauseMenuCanvas.SetActive(false);
    }
}
