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
                }
            }
            else
            {
                Debug.LogWarning("NotesPlaceholdersContainer not found in the scene.");
            }

            // if (TestPlaceholder != null)
            // {
            //     MusicalOctaveNote octaveNote = HeightToNote[TestPlaceholder.transform.position.y];
            //     // Debug.Log($"Octaves: {Octaves}");
            //     // Debug.Log($"Octaves[note.Octave]: {Octaves[note.Octave]}");
            //     // Debug.Log($"Octaves[note.Octave].Notes: {Octaves[note.Octave].Notes}");
            //     // Debug.Log($"Octaves[note.Octave].Notes[note.Note]: {Octaves[note.Octave].Notes[note.Note]}");
            //     // Debug.Log($"Prefab: {Octaves[note.Octave].Notes[note.Note].Prefab}");
            //     // Debug.Log($"Position: {child.position}");
            //     // Debug.Log($"Rotation: {child.rotation}");
            //     // Debug.Log($"Parent: {NotesPlaceholdersContainer.transform}");
            //     GameObject noteGameObject = Instantiate(
            //         Octaves[octaveNote.Octave].Notes[octaveNote.Note].Prefab,
            //         TestPlaceholder.transform.position,
            //         TestPlaceholder.transform.rotation,
            //         NotesPlaceholdersContainer.transform
            //     );

            //     Destroy(TestPlaceholder);
            //     MusicalNoteCollectable collectable = noteGameObject.GetComponent<MusicalNoteCollectable>();
            //     collectable.SetOctave(octaveNote.Octave);
            //     collectable.OnCollectNoteEvent += HandleCollectNote;
            // }
            // else
            // {
            //     Debug.LogWarning("TestPlaceholder not found in the scene.");
            // }
        }

        protected void HandleCollectNote(MusicalOctave octave, MusicalNote note, int noteValue)
        {
            scoreManager.AddCoinCounter();
            scoreManager.UpdateScore(noteValue);
            AudioSource.clip = Octaves[octave].Notes[note].Instruments[CurrentInstrument].AudioClip;
            AudioSource.Play();
        }
    }
}