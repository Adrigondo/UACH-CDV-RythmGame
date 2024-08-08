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

        void Start()
        {
            AudioSource = GetComponent<AudioSource>();


            if (NotesPlaceholdersContainer != null)
            {
                foreach (Transform child in NotesPlaceholdersContainer.transform)
                {
                    MusicalOctaveNote note = HeightToNote[child.position.y];

                    GameObject noteGameObject = Instantiate(
                        Octaves[note.Octave].Notes[note.Note].Prefab,
                        child.position,
                        child.rotation,
                        NotesPlaceholdersContainer.transform
                    );
                    // Debug.Log((note, noteGameObject));
                    // Debug.Log(child, noteGameObject);
                    Destroy(child.gameObject);

                    MusicalNoteCollectable collectable = noteGameObject.GetComponent<MusicalNoteCollectable>();
                    collectable.OnCollectNoteEvent += HandleCollectNote;

                }
            }
            else
            {
                Debug.LogWarning("GameObject '@Placeholders' not found in the scene.");
            }
        }

        protected void HandleCollectNote(MusicalOctaveNote octaveNote)
        {
            Debug.Log(octaveNote);
            AudioSource.clip = Octaves[octaveNote.Octave].Notes[octaveNote.Note].Instruments[CurrentInstrument].AudioClip;
            AudioSource.Play();
        }
    }
}