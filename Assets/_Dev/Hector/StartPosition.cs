using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{
    // protected Vector3 startPositon;

    void Start()
    {
        // startPositon = transform.position;
    }

    public Vector3 GetStartPosition()
    {
        return transform.position;
    }
}
