using TMPro;
using UnityEngine;

public class ScriptableObjectRecieve : MonoBehaviour
{
    [SerializeField][Tooltip("A GameObject with a SceneChanger Script")]protected SceneChanger sceneChanger;
    [SerializeField][Tooltip("The text of the level name")] protected TMP_Text LevelName;
    [SerializeField][Tooltip("The maximum score of the level")] protected TMP_Text MaximumLevelScore;
    [SerializeField][Tooltip("Youre Score Of the Level")] protected TMP_Text YoureScore;
    [SerializeField][Tooltip("Youre how many times died Level")] protected TMP_Text ManyDieds;
    [SerializeField][Tooltip("The porc of the level")] protected TMP_Text PorcOfTheLevel;
    [SerializeField][Tooltip("The range of the Level")] protected TMP_Text LevelLetter;
    [SerializeField][Tooltip("The text of the coins collected")] protected TMP_Text CoinsCollected;
    [SerializeField][Tooltip("The text of the time played")] protected TMP_Text Timeplayed;
    private float porcent;
    private float timePlayed;
    private LevelsScriptableObject nextlevelScriptableObjectStored;
    void Start()
    {
        timePlayed = 0;
    }
    void Update()
    {
        timePlayed += Time.deltaTime;
    }
    public void Recieve(LevelsScriptableObject levelsScriptableObject, LevelsScriptableObject nextlevelScriptableObject, int score, int deathCounter, int coinsCollected)
    {
        nextlevelScriptableObjectStored = nextlevelScriptableObject;
        LevelName.text = "LEVEL NAME: " + levelsScriptableObject.LevelName;
        MaximumLevelScore.text = "MAXIMUM SCORE: " + levelsScriptableObject.MaximumScore.ToString();
        YoureScore.text = "YOURE SCORE WAS: " + score.ToString();
        CoinsCollected.text = coinsCollected.ToString();
        Timeplayed.text = "TIME PLAYED: " + timePlayed.ToString();
        if (deathCounter == 1)
        {
            ManyDieds.text = "YOU HAVE DIED: " + deathCounter.ToString() + " TIME";
        }
        else
        {
            ManyDieds.text = "YOU HAVE DIED: " + deathCounter.ToString() + " TIMES";
        }
        porcent = score*100 / levelsScriptableObject.MaximumScore;
        PorcOfTheLevel.text = porcent.ToString() + "%";
        if (porcent == 100)
        {
            LevelLetter.text = "S";
        }
        else if ((porcent<=99) && (porcent>80))
        {
            LevelLetter.text = "A";
        }
        else if ((porcent<=79) && (porcent>60))
        {
            LevelLetter.text = "B";
        }
        else if ((porcent<=59) && (porcent>40))
        {
            LevelLetter.text = "C";
        }
        else if ((porcent<=39) && (porcent>20))
        {
            LevelLetter.text = "C";
        }
        else
        {
            LevelLetter.text = "D";
        }
    }
    public void NextLevel()
    {
        sceneChanger.ChangeSceneWithCode(nextlevelScriptableObjectStored.LevelScene);
    }
}
