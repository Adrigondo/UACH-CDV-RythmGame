using UnityEngine;

public class FinishGameUI : MonoBehaviour
{
    [SerializeField][Tooltip("A GameObject that has a ScriptableObjectRecive script in it")] protected ScriptableObjectRecieve scriptableObjectRecieve;
    [SerializeField][Tooltip("Here goes the ScriptableObject of the level")] protected LevelsScriptableObject levelsScriptableObject;
    [SerializeField][Tooltip("Here goes the ScriptableObject of the next level")] protected LevelsScriptableObject nextlevelScriptableObject;
    [SerializeField][Tooltip("Here goes a GameObject that has a ScoreManager script in it")] protected ScoreManager scoreManager;
    [SerializeField][Tooltip("Here goes the Player")] protected NewPlayerBehavior newPlayerBehavior;
    [SerializeField][Tooltip("The End Game Pannel")] protected GameObject endgamePannel;

    void Start()
    {
        endgamePannel.SetActive(false);
    }

    public void ChangeEndGameUI()
    {
        scriptableObjectRecieve.Recieve(levelsScriptableObject, nextlevelScriptableObject, scoreManager.score, scoreManager.deathCounter, scoreManager.coinCuantity);
        endgamePannel.SetActive(true);
    }
}
