using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    public float offsetX = 5f;

    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x + offsetX, 0f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
