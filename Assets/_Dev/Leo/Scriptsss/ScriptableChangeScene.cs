using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScriptableChangeScene : MonoBehaviour
{
    public LevelsScriptableObject levelsScriptableObject;
    public SceneChanger sceneChanger;
    void Update()
    {
        sceneChanger.ChangeSceneWithCode(levelsScriptableObject.LevelScene);
    }
}
