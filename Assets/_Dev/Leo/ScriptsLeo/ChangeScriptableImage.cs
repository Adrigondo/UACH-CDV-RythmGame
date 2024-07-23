using UnityEngine;
using UnityEngine.UI;

public class ChangeScriptableImage : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The ScriptableObject for your level.")]
    protected LevelsScriptableObject levelsScriptableObject;
    Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = levelsScriptableObject.LevelImages[0];
    }
}
