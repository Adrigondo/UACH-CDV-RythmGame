using TMPro;
using UnityEngine;

public class ScriptableObjectRecieve : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text of the level name")]
    protected TMP_Text LevelName;
    [SerializeField]
    [Tooltip("The maximum score of the level")]
    protected TMP_Text MaximumLevelScore;
    public void Recieve(LevelsScriptableObject levelsScriptableObject)
    {
        LevelName.text = "LEVEL NAME: " + levelsScriptableObject.LevelName;
        MaximumLevelScore.text = "MAXIMUM SCORE: " + levelsScriptableObject.MaximumScore.ToString();
    }
}
