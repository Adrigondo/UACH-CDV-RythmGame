using UnityEngine;

public class ChangeScpritableScene : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ScriptableObject for your level.")]
    protected LevelsScriptableObject levelsScriptableObject;

    [SerializeField]
    [Tooltip("SceneChanger gameobject of the current scene.")]
    protected SceneChanger sceneChanger;

    void Update()
    {
        sceneChanger.ChangeSceneWithCode(levelsScriptableObject.LevelScene);
    }
}
