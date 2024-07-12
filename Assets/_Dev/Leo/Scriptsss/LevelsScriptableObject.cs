using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.SearchService;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelScriptableobject", menuName = "ScriptableObjects/Level")]

public class LevelsScriptableObject : ScriptableObject
{
    [SerializeField]
    string levelName;
    public string LevelName {get => levelName; private set => levelName = value;}

    [SerializeField]
    string levelDescription;
    public string LevelDescription {get => levelDescription; private set => levelDescription = value;}

    [SerializeField]
    AudioClip musicLevel;
    public AudioClip MusicLevel {get => musicLevel; private set => musicLevel = value;}

    [SerializeField]
    float maximumscore;
    public float MaximumScore {get => maximumscore; private set => maximumscore = value;}

    [SerializeField]
    string levelScene;
    public string LevelScene {get => levelScene; private set => levelScene = value;}
}