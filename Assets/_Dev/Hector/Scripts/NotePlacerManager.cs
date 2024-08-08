using System.Collections.Generic;
using UnityEngine;

public class NotePlacerManager : MonoBehaviour
{
    [SerializeField] protected GameObject notePlaceholders;
    [SerializeField] protected GameObject[] notePrefabs;
    private Dictionary<float, int> heightToNoteCorrespondance;

    void Awake()
    {
        // The 'int' in this variable corresponds to the index of the note in the 'notePrefabs' variable
        // The values of each index and their note are as follows:
        // 0 : Do
        // 1 : Re
        // 2 : Mi
        // 3 : Fa
        // 4 : Sol
        // 5 : La
        // 6 : Si
        heightToNoteCorrespondance = new Dictionary<float, int>
        {
            { -1.94f, 1 },
            { -1.949592f, 2 },
            { -1.949592f, 3 },
            { -1.949592f, 4 },
            { -1.949592f, 5 },
            { -1.949592f, 6 },
            { -1.949592f, 1 },
            { -1.949592f, 1 } 
        };
    }

    void Start()
    {

        if (notePlaceholders != null)
        {
            foreach (Transform child in notePlaceholders.transform)
            {
                GameObject childObject = child.gameObject;
                Debug.Log($"Child GameObject Position: {childObject.transform.position.y}");
                // TODO: ADD THE CODE RESPONSIBLE FOR CREATING AN INSTANCE OF THE RESPECTIVE NOTE BASED ON HEIGHT

                Destroy(childObject);
            }
        }
        else
        {
            Debug.LogWarning("GameObject '@Placeholders' not found in the scene.");
        }
    }
}
