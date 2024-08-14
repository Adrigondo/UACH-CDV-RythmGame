using UnityEngine;
[CreateAssetMenu(fileName = "NoteCriptableObject", menuName = "ScriptableObjects/Note")]

public class NotesScriptableObject : ScriptableObject
{
    [SerializeField] string noteName;
    public string NoteName {get => noteName; private set => noteName = value;}
    [SerializeField] int noteValue;
    public int NoteValue {get => noteValue; private set => noteValue = value;}
    [SerializeField] AudioClip noteSound;
    public AudioClip NoteSound {get => noteSound; private set => noteSound = value;}
}
