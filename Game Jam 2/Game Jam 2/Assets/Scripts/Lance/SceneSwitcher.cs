using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void OnClickTitleStart()
    {
        SceneManager.LoadScene("Intro_Scene");
    }

    public void OnClickTitleExit()
    {
        Application.Quit();
    }
    public void OnClickFinishIntro()
    {
        SceneManager.LoadScene("Lance's Workspace");
    }

    public void OnClickBackToTitle()
    {
        SceneManager.LoadScene("Title");
        Time.timeScale = 1.0f;
    }

}
