using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    /* [SerializeField] private Transform target;
    private PlayerAutoscrollScript targetScript;
    private float cameraFollowSpeed = 5f;
    [SerializeField] private float maxCameraMarginDistance = -6.5f;
    [SerializeField] private float minCameraMarginDistance = 4.28f;
    private Vector3 offset;

    void Start()
    {
        targetScript = target.GetComponent<PlayerAutoscrollScript>();
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset + calculateDynamicCameraOffset();
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }

    private Vector3 calculateDynamicCameraOffset()
    {
        float targetMovementAngle = targetScript.GetMovementAngleInRadians();
        Vector2 movementDirection = new Vector2(Mathf.Cos(targetMovementAngle), Mathf.Sin(targetMovementAngle));

        UpdateCameraValuesDependingOnDirection(targetScript.GetIsMovingForward(), movementDirection);
        float normalizedAngle = Mathf.Abs(targetMovementAngle) / (Mathf.PI / 2);
        float dynamicMarginDistance = Mathf.Lerp(maxCameraMarginDistance, minCameraMarginDistance, normalizedAngle * normalizedAngle);
        Vector3 dynamicOffset = movementDirection * dynamicMarginDistance;

        return dynamicOffset;
    }

    void UpdateCameraValuesDependingOnDirection(bool isMovingForwards, Vector2 movementDirection)
    {
        if (isMovingForwards)
        {
            offset = transform.position - target.position;
            maxCameraMarginDistance = -6.5f;
        }
        else
        {
            offset = target.position - transform.position;
            maxCameraMarginDistance = 6.5f;
        }
    } */


    [SerializeField] private Transform target;
    private PlayerAutoscrollScript targetScript;
    private float cameraFollowSpeed = 5f;
    [SerializeField] private float maxCameraMarginDistance = 6.5f;
    [SerializeField] private float minCameraMarginDistance = 4.28f;
    private Vector3 offset;
    private bool lastIsMovingForwardsRegistered;
    private float cameraXOffset = 13f;

    void Start()
    {
        targetScript = target.GetComponent<PlayerAutoscrollScript>();
        offset = transform.position - target.position;
        
        lastIsMovingForwardsRegistered = targetScript.GetIsMovingForward();
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset + calculateDynamicCameraOffset();
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }

    private Vector3 calculateDynamicCameraOffset()
    {
        float targetMovementAngle = targetScript.GetMovementAngleInRadians();
        Vector2 movementDirection = new Vector2(Mathf.Cos(targetMovementAngle), Mathf.Sin(targetMovementAngle));

        bool currentDirection = targetScript.GetIsMovingForward();
        if (currentDirection != lastIsMovingForwardsRegistered)
        {
            lastIsMovingForwardsRegistered = currentDirection;
            if (currentDirection)
                offset.x += cameraXOffset;
            else
                offset.x -= cameraXOffset;
        }

        float normalizedAngle = Mathf.Abs(targetMovementAngle) / (Mathf.PI / 2);
        float dynamicMarginDistance = Mathf.Lerp(maxCameraMarginDistance, minCameraMarginDistance, normalizedAngle * normalizedAngle);
        Vector3 dynamicOffset = movementDirection * dynamicMarginDistance;

        return dynamicOffset;
    }

    void UpdateCameraValuesDependingOnDirection(bool isMovingForwards, Vector2 movementDirection)
    {

    }




/*     [SerializeField] private Transform target;
    private PlayerAutoscrollScript targetScript;
    private float cameraFollowSpeed = 5f;
    private float cameraMarginDistance;
    private Vector3 offset;

    void Start()
    {
        targetScript = target.GetComponent<PlayerAutoscrollScript>();
    }

    void LateUpdate()
    {
        float targetMovementAngle = targetScript.GetMovementAngleInRadians();
        Vector2 movementDirection = new Vector2(Mathf.Cos(targetMovementAngle), Mathf.Sin(targetMovementAngle));

        Vector3 targetPosition = UpdateCameraValuesDependingOnDirection(targetScript.GetIsMovingForward(), movementDirection);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }

    Vector3 UpdateCameraValuesDependingOnDirection(bool isMovingForwards, Vector2 movementDirection)
    {
        Vector3 result;
        if (isMovingForwards)
        {
            offset = transform.position - target.position;
            cameraMarginDistance = 6.5f;
            Vector3 dynamicOffset = new Vector3(movementDirection.x, movementDirection.y, 0) * cameraMarginDistance;
            result = target.position + offset + dynamicOffset;
        }
        else
        {
            offset = target.position - transform.position;
            cameraMarginDistance = 5f;
            Vector3 dynamicOffset = new Vector3(movementDirection.x, movementDirection.y, 0) * cameraMarginDistance;
            result = target.position - offset - dynamicOffset;
        }
        return result;
    } */



    /* [SerializeField] private Transform target;
    private PlayerAutoscrollScript targetScript;
    private float cameraFollowSpeed = 5f;
    private float cameraMarginDistance;
    private Vector3 offset;

    void Start()
    {
        targetScript = target.GetComponent<PlayerAutoscrollScript>();
    }

    void LateUpdate()
    {
        float targetMovementAngle = targetScript.GetMovementAngleInRadians();
        Vector2 movementDirection = new Vector2(Mathf.Cos(targetMovementAngle), Mathf.Sin(targetMovementAngle));

        Vector3 targetPosition = UpdateCameraValuesDependingOnDirection(targetScript.GetIsMovingForward(), movementDirection);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }

    Vector3 UpdateCameraValuesDependingOnDirection(bool isMovingForwards, Vector2 movementDirection)
    {
        Vector3 result;
        if (isMovingForwards)
        {
            offset = transform.position - target.position;
            cameraMarginDistance = 6.5f;
            Vector3 dynamicOffset = new Vector3(movementDirection.x, movementDirection.y, 0) * cameraMarginDistance;
            result = target.position + offset + dynamicOffset;
        }
        else
        {
            offset = target.position - transform.position;
            cameraMarginDistance = 4.5f;
            Vector3 dynamicOffset = new Vector3(movementDirection.x, movementDirection.y, 0) * cameraMarginDistance;
            result = target.position - offset - dynamicOffset;
        }
        return result;
    } */
}
