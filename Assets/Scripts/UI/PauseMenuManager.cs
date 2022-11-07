using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public void OnResumeButtonClicked()
    {
        GameManager.instance.InGame();
    }

    public void OnRestartButtonClicked()
    {
        string currSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currSceneName);
    }

    public void OnExitButtonClicked()
    {
        string mainMenuSceneName = "Main Menu";
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
