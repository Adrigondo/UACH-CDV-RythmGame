using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythmGame
{
    public class MusicalNotesCollectableByPlayer : MonoBehaviour
    {
        [SerializeField] protected MusicalNotesManager musicalNotesManager;
        [SerializeField] protected NewPlayerBehavior player;
        void OnEnable(){
            player.OnDeathEvent+=musicalNotesManager.RespawnNotes;
        }
        

    }
}
