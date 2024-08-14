using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace RythmGame
{
    public class MusicalNotesManager : MonoBehaviour
    {
        public MusicalInstrument CurrentInstrument = MusicalInstrument.Guitar1;
        [SerializeField]
        [SerializedDictionary("Octave", "Data")]
        protected SerializedDictionary<MusicalOctave, MusicalOctaveData> Octaves;

        [SerializeField]
        [SerializedDictionary("Heights", "Notes")]
        protected SerializedDictionary<float, MusicalOctaveNote> HeightToNote;

        [SerializeField] protected GameObject NotesPlaceholdersContainer;

        protected AudioSource AudioSource;
        [SerializeField] protected ScoreManager scoreManager;

        protected delegate void OnRespawn();
        protected OnRespawn OnRespawnEvent;

        void Start()
        {
            AudioSource = GetComponent<AudioSource>();


            if (NotesPlaceholdersContainer != null)
            {
                Transform[] children=NotesPlaceholdersContainer.GetComponentsInChildren<Transform>();
                int lenght=children.Length;
                for (int i=1; i<lenght; i++)
                {
                    MusicalOctaveNote octaveNote = HeightToNote[children[i].position.y];
                    
                    GameObject noteGameObject = Instantiate(
                        Octaves[octaveNote.Octave].Notes[octaveNote.Note].Prefab,
                        children[i].position,
                        children[i].rotation,
                        NotesPlaceholdersContainer.transform
                    );
                    Destroy(children[i].gameObject);

                    MusicalNoteCollectable collectable = noteGameObject.GetComponent<MusicalNoteCollectable>();
                    collectable.SetOctave(octaveNote.Octave);
                    collectable.OnCollectNoteEvent += HandleCollectNote;
                    OnRespawnEvent+=collectable.Respawn;
                }
            }
            else
            {
                Debug.LogWarning("NotesPlaceholdersContainer not found in the scene.");
            }
        }

        protected void HandleCollectNote(MusicalOctave octave, MusicalNote note, int noteValue)
        {
            scoreManager.AddCoinCounter();
            scoreManager.UpdateScore(noteValue);
            AudioSource.clip = Octaves[octave].Notes[note].Instruments[CurrentInstrument].AudioClip;
            AudioSource.Play();
        }

        public void RespawnNotes(){
            OnRespawnEvent?.Invoke();
        }
    }
}