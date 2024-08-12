using UnityEngine;

public class PauseButtonScrip : MonoBehaviour
{
    [SerializeField][Tooltip("The Pause Pannel in canvas")] GameObject PausePannel;
    [SerializeField][Tooltip("The @AudioVisualizator prefab for the music")] AudioWaveforms audioWaveforms;
    [SerializeField][Tooltip("Here goes the player")] NewPlayerBehavior newPlayerBehavior;
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
        Time.timeScale = 1f;
        PausePannel.SetActive(false);
        audioWaveforms.audioSource.Play();
    }
    public void RestartGame()
    {
        PausePannel.SetActive(false);
        Time.timeScale = 1f;
        newPlayerBehavior.Death();
    }
    public void StartGamePaused()
    {
        audioWaveforms.audioSource.Pause();
        Time.timeScale = 0f;
    }
}
