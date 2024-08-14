
using UnityEngine;

namespace RythmGame
{
    public class MusicalNoteCollectable : ScoredCollectableOnTriggerByPlayer
    {
        public delegate void OnCollectNote(MusicalOctave octave, MusicalNote note);
        public OnCollectNote OnCollectNoteEvent;

        [Tooltip("The musical note and octave that represents this gameobject")]
        [SerializeField] protected MusicalNote note;
        protected MusicalOctave octave;

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnCollectNoteEvent?.Invoke(octave, note);
                OnCollectEvent?.Invoke(ScoreValue);
                // scoreManager.AddCoinCounter();
                Dissappear();
            }
        }
        public void SetOctave(MusicalOctave octave)
        {
            this.octave = octave;
        }


        // [SerializeField] AudioSource audioSource;
        // override void Dissappear(){

        // }
    }
}
