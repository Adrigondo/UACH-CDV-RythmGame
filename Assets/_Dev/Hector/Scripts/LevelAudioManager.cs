using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    #region "Fields"
    [SerializeField] protected AudioSource musicAudioSource;
    #endregion

    #region "Properties"
    #endregion

    #region "LifeCycle Methods"
    protected void Start()
    {
        if (CheckForMissingComponent<AudioSource>(musicAudioSource))
            Debug.LogError($"{musicAudioSource} NOT FOUND IN PROJECT");
    }
    #endregion

    #region "Public Methods"
    #endregion

    #region "Protected Methods"
    protected bool CheckForMissingComponent<T>(T obj) where T : class
    {
        if (obj == null)
            return false;
        return true;
    }

    protected void RespawnPlayer()
    {
    }

    #endregion
}
