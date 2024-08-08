using System;
using UnityEngine;

namespace RythmGame
{
    [Serializable]
    public class MusicalOctaveNote
    {
        [SerializeField] 
        public MusicalOctave Octave;
        
        [SerializeField] 
        public MusicalNote Note;
    }
}