using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChenger : MonoBehaviour
{
    public LevelsScriptableObject levelsScriptableObject;
    Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = levelsScriptableObject.LevelImages[0];
    }
}
