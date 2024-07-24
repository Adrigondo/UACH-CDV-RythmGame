using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoscrollScript : MonoBehaviour
{
    [SerializeField] private bool isMovingForwards = true;
    [SerializeField] private float movementAngleInDegrees = 0;
    [SerializeField] private float movementSpeed = 5f;
    private float movementAngleInRadians = 0;

    void Update()
    {
        ClampAngleToBoundary();
        movementAngleInRadians = movementAngleInDegrees * Mathf.Deg2Rad;
        Vector2 movement = new Vector2(Mathf.Cos(movementAngleInRadians), Mathf.Sin(movementAngleInRadians));
        if (!isMovingForwards)
            movement.x *= -1;

        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    private void ClampAngleToBoundary()
    {
        if (movementAngleInDegrees > 90)
            movementAngleInDegrees = 90;
        if (movementAngleInDegrees < -90)
            movementAngleInDegrees = -90;
    }

    public float GetMovementAngleInRadians()
    {
        return movementAngleInRadians;
    }

    public bool GetIsMovingForward()
    {
        return isMovingForwards;
    }
}
