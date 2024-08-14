using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour, IDataPersistance
{
    protected const int DEATHLESSBONUS = 1000;
    [SerializeField] protected int coinValue = 10;
    [HideInInspector] public int score;
    [HideInInspector] public int deathCounter;
    [HideInInspector] public int coinCuantity;
    protected int deathPenaltyFinalScore;
    private Dictionary<string, int> collectableScores;
    [SerializeField] protected TMP_Text scoreLabel;

    void Start()
    {
        coinCuantity = 0;
        if (scoreLabel == null)
            Debug.LogError("No Score Label found on Score Manager");

        RestartScore();
        RestartDeathCounter();
    }

    public void AddCoinCounter()
    {
        coinCuantity ++;
        SetScoreLabel(score);
    }

    public void UpdateScore(int scoreSended)
    {
        score += scoreSended;
    }

    public int AddUpFinalScore()
    {
        deathPenaltyFinalScore = (DEATHLESSBONUS - (deathCounter * 100)) < 0 ? 0 : (DEATHLESSBONUS - (deathCounter * 100));
        score += deathPenaltyFinalScore;

        return score;
    }

    public void AddDeathToCounter()
    {
        deathCounter++;
        coinCuantity = 0;
        RestartScore();
    }

    public int GetScore()
    {
        return score;
    }

    protected void RestartScore()
    {
        score = 0;
        SetScoreLabel(score);
    }

    protected void RestartDeathCounter()
    {
        deathCounter = 0;
    }

    protected void SetScoreLabel(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void LoadData(GameData data)
    {
        // Nothing to do in this case. This function was only added because of the interface needed for saving.
    }

    public void SaveData(ref GameData data)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int levelIndex = int.Parse(sceneName.Substring(sceneName.Length - 2)) - 1;

        data.LevelScore[levelIndex] = AddUpFinalScore();
    }
}
