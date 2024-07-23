using UnityEngine;

public class PauseButtonScrip : MonoBehaviour
{
    [SerializeField]
    GameObject PausePannel;
    void Start()
    {
        PausePannel.SetActive(false);
    }

    public void PauseGame()
    {

        Time.timeScale = 0f;
        PausePannel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PausePannel.SetActive(false);
    }
}
