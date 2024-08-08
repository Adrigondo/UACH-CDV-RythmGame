using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;


namespace RythmGame
{
    [Serializable]
    public class MusicalNoteData
    {
        [SerializedDictionary("Instrument", "Data")]
        public SerializedDictionary<MusicalInstrument, MusicalInstrumentData> Instruments;
        
        [SerializeField]
        public GameObject Prefab;
    }
}