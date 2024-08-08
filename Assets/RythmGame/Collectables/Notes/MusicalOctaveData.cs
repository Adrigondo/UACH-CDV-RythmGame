using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;


namespace RythmGame
{
    [Serializable]
    public class MusicalOctaveData
    {
        [SerializedDictionary("Note", "Data")]
        public SerializedDictionary<MusicalNote, MusicalNoteData> Notes;
    }
}