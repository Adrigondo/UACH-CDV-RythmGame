using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] protected Transform target;
    protected Vector3 startPosition;
    protected float followSpeed = 2f;
    protected float offsetX = 5f;

    protected void Start()
    {
        startPosition = transform.position;
    }

    protected void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x + offsetX, 0f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }

    public void RestartCamera()
    {
        transform.position = startPosition;
    }
}
