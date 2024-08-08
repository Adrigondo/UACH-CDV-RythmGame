
using UnityEngine;

namespace RythmGame
{
    public class MusicalNoteCollectable : ScoredCollectableOnTriggerByPlayer
    {
        public delegate void OnCollectNote(MusicalOctaveNote octaveNote);
        public OnCollectNote OnCollectNoteEvent;

        [Tooltip("The musical note and octave that represents this gameobject")]
        [SerializeField] protected MusicalOctaveNote OctaveNote;

        public override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("COLLECTED");
                OnCollectNoteEvent?.Invoke(OctaveNote);
                OnCollectEvent?.Invoke(ScoreValue);
                Dissappear();
            }
        }

        // [SerializeField] AudioSource audioSource;
        // override void Dissappear(){

        // }
    }
}
