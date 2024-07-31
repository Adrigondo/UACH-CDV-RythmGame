using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float MusicVolume;
    public float SFXVolume;

    public GameData()
    {
        this.MusicVolume = 1;
        this.SFXVolume = 1;
    }
}
