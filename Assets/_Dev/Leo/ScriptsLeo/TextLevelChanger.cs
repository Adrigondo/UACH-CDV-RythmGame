using UnityEngine;
using TMPro;

public class TextLevelChanger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ScriptableObject for your level.")]
    protected LevelsScriptableObject levelsScriptableObject;

    [SerializeField]
    [Tooltip("The text to change your Level Number")]
    protected TMP_Text LevelNumber;

    [SerializeField]
    [Tooltip("The text to change your Level Name")]
    protected TMP_Text LevelName;

    [SerializeField]
    [Tooltip("The text to change your maximum Level Score")]
    protected TMP_Text LevelScore;
    
    void Start()
    {
        LevelNumber.text = "Level: " + levelsScriptableObject.LevelNumber.ToString();
        LevelName.text = "Name: " + levelsScriptableObject.LevelName;
        LevelScore.text = "Maximum Score: " + levelsScriptableObject.MaximumScore.ToString();
    }
}
