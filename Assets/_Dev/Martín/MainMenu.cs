using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelsMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void GarmentsMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void SettingsScene()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
