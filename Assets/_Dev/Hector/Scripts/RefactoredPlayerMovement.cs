using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefactoredPlayerMovement : MonoBehaviour
{
    #region "Fields"
    [SerializeField] protected StartPosition startPositionScript;
    [SerializeField] protected LevelAudioManager levelAudioManager;

    #endregion

    #region "Properties"
    #endregion

    #region "LifeCycle Methods"
    protected void Start()
    {
        RespawnPlayer();
    }
    #endregion

    #region "Public Methods"
    #endregion

    #region "Protected Methods"
    protected void RespawnPlayer()
    {
        transform.position = startPositionScript.GetStartPosition();

        /* if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play(); */
    }

    #endregion
}
