using UnityEngine;

public class PauseButtonScrip : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Pause Pannel in canvas")]
    GameObject PausePannel;
    [SerializeField]
    [Tooltip("The @AudioVisualizator prefab for the music")]
    AudioWaveforms audioWaveforms;
    [SerializeField]
    [Tooltip("Here goes the player")]
    NewPlayerBehavior newPlayerBehavior;
    void Start()
    {
        PausePannel.SetActive(false);
    }

    public void PauseGame()
    {
        audioWaveforms.audioSource.Pause();
        Time.timeScale = 0f;
        PausePannel.SetActive(true);
    }
    public void ResumeGame()
    {
        PausePannel.SetActive(false);
        audioWaveforms.audioSource.Play();
        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        PausePannel.SetActive(false);
        Time.timeScale = 1f;
        newPlayerBehavior.Death();
    }
}
