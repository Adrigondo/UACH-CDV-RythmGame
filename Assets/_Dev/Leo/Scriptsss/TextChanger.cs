using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    public LevelsScriptableObject levelsScriptableObject;
    public TMP_Text LevelNumber;
    public TMP_Text LevelName;
    public TMP_Text LevelScore;

    void Start()
    {
        LevelNumber.text = "Level: " + levelsScriptableObject.LevelNumber.ToString();
        LevelName.text = "Name: " + levelsScriptableObject.LevelName;
        LevelScore.text = "Maximum Score: " + levelsScriptableObject.MaximumScore.ToString();
    }
}
