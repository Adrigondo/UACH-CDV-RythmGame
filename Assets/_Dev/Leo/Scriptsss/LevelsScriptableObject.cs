using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "LevelScriptableobject", menuName = "ScriptableObjects/Level")]

public class LevelsScriptableObject : ScriptableObject
{
    [SerializeField]
    string levelName;
    public string LevelName {get => levelName; private set => levelName = value;}

    [SerializeField]
    int levelNumber;
    public int LevelNumber {get => levelNumber; private set => levelNumber = value;}

    [SerializeField]
    AudioClip musicLevel;
    public AudioClip MusicLevel {get => musicLevel; private set => musicLevel = value;}

    [SerializeField]
    float maximumscore;
    public float MaximumScore {get => maximumscore; private set => maximumscore = value;}

    [SerializeField]
    List<Sprite> levelImages = new List<Sprite>(3);
    public List<Sprite> LevelImages {get => levelImages; private set => levelImages = value;}

    [SerializeField]
    string levelScene;
    public string LevelScene {get => levelScene; private set => levelScene = value;}
}