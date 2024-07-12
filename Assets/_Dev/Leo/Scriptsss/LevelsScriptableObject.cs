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
    Image levelImage;
    public Image LevelImage {get => levelImage; private set => levelImage = value;}

    [SerializeField]
    float maximumscore;
    public float MaximumScore {get => maximumscore; private set => maximumscore = value;}

    [SerializeField]
    string levelScene;
    public string LevelScene {get => levelScene; private set => levelScene = value;}
}
