using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    protected const float baseMovementSpeed = 4f;
    [SerializeField] protected int songBPM;
    [SerializeField] protected NewPlayerBehavior playerBehavior;

    void Start()
    {
        float newMovementSpeed = baseMovementSpeed * ((float)songBPM / 60);
        playerBehavior.SetMovementSpeed(newMovementSpeed);
    }
}
