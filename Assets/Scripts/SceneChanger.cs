using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Scene;
    public static SceneChanger Instance;

    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeSceneWithCode(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
