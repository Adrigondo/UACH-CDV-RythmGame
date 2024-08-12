using TMPro;
using UnityEngine;

public class ScreenCounter : MonoBehaviour
{
    [SerializeField][Tooltip("The LoadPannel")] protected GameObject LoadPannel;
    [SerializeField][Tooltip("The number you want to change")] protected TMP_Text counterText;
    [SerializeField][Tooltip("A GameObject that has a PauseButtonScript in it")] protected PauseButtonScrip pauseButtonScrip;
    [SerializeField][Tooltip("The counter for the screen")] protected float WaitTime;
    private float counter;
    void Start () 
    {
        pauseButtonScrip.StartGamePaused();
        counter = 0;
        LoadPannel.SetActive(true);
    }
    void Update ()
    {
        if (counter >= WaitTime)
        {
            pauseButtonScrip.ResumeGame();
            LoadPannel.SetActive(false);
        }
        counter += Time.fixedDeltaTime;
        counterText.text = counter.ToString("F1");
    }
}
