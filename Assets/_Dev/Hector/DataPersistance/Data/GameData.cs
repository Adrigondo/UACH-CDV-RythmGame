using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float MusicVolume;
    public float SFXVolume;
    public int[] LevelScore;

    public GameData()
    {
        this.LevelScore = new int[2] { 0 , 0 };

        this.MusicVolume = 1;
        this.SFXVolume = 1;
    }
}
