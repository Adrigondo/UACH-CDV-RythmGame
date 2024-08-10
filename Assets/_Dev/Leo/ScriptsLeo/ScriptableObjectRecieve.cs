using TMPro;
using UnityEngine;

public class ScriptableObjectRecieve : MonoBehaviour
{
    [SerializeField][Tooltip("The text of the level name")] protected TMP_Text LevelName;
    [SerializeField][Tooltip("The maximum score of the level")] protected TMP_Text MaximumLevelScore;
    [SerializeField][Tooltip("Youre Score Of the Level")] protected TMP_Text YoureScore;
    [SerializeField][Tooltip("Youre how many times died Level")] protected TMP_Text ManyDieds;
    [SerializeField][Tooltip("The porc of the level")] protected TMP_Text PorcOfTheLevel;
    [SerializeField][Tooltip("The range of the Level")] protected TMP_Text LevelLetter;
    [SerializeField][Tooltip("The text of the coins collected")] protected TMP_Text CoinsCollected;
    private float Porcent;
    public void Recieve(LevelsScriptableObject levelsScriptableObject, int score, int deathCounter, int coinsCollected)
    {
        LevelName.text = "LEVEL NAME: " + levelsScriptableObject.LevelName;
        MaximumLevelScore.text = "MAXIMUM SCORE: " + levelsScriptableObject.MaximumScore.ToString();
        YoureScore.text = "YOURE SCORE WAS: " + score.ToString();
        CoinsCollected.text = coinsCollected.ToString();
        if (deathCounter == 1)
        {
            ManyDieds.text = "YOU HAVE DIED: " + deathCounter.ToString() + " TIME";
        }
        else
        {
            ManyDieds.text = "YOU HAVE DIED: " + deathCounter.ToString() + " TIMES";
        }
        Porcent = score*100 / levelsScriptableObject.MaximumScore;
        PorcOfTheLevel.text = Porcent.ToString() + "%";
        if (Porcent == 100)
        {
            LevelLetter.text = "S";
        }
        else if ((Porcent<=99) && (Porcent>80))
        {
            LevelLetter.text = "A";
        }
        else if ((Porcent<=79) && (Porcent>60))
        {
            LevelLetter.text = "B";
        }
        else if ((Porcent<=59) && (Porcent>40))
        {
            LevelLetter.text = "C";
        }
        else if ((Porcent<=39) && (Porcent>20))
        {
            LevelLetter.text = "C";
        }
        else
        {
            LevelLetter.text = "D";
        }
    }
}
