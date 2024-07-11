using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButtonScript : MonoBehaviour
{
    public GameObject buttonController;

    void Start()
    {
        buttonController.SetActive(false);   
    }
    void Update()
    {
        if (buttonController.SetActive(true))
        {
            
        }
    }
}
