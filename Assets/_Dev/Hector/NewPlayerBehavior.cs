// TODO: MAKE THIS ACTUALLY WORK FOR WHEN THE PLAYER IS UPSIDE-DOWN OR RIGHTSIDE-UP.
// TODO: MAKE THIS WORK FOR ALL ANGLES.
// TODO: I SHOULD PROBABLY MAKE THE TELEPORT ITSELF INTO A DIFFERENT FUNCTION OR SOMETHING IDK.
// TODO: NONE OF THIS WORKS IF THERE IS A COLLIDER THAT ISN'T WALKABLE IN THE WAY OF THE ACTUALLY WALKABLE GROUND.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerBehavior : MonoBehaviour
{
    [SerializeField] protected float movementAngleInDegrees = 0;
    [SerializeField] protected float movementSpeed = 5f;
    protected float movementAngleInRadians = 0;
    protected Collider2D playerCollider;
    protected Rigidbody2D rigidBody2D;
    protected float playerHeight;
    [SerializeField] protected float raycastLength = 10f;

    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        if (playerCollider != null)
        {
            playerHeight = playerCollider.bounds.size.y;
        }
        else
        {
            Debug.LogError("No Collider2D found on the player");
        }
    }

    void Update()
    {
        movementAngleInRadians = movementAngleInDegrees * Mathf.Deg2Rad;
        Vector2 movement = new Vector2(Mathf.Cos(movementAngleInRadians), Mathf.Sin(movementAngleInRadians));
        transform.Translate(movement * movementSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            CheckForWalkableTerrainAbove();
        }
    }

    /// <summary>
    // The following function makes use of a RaycastHit2D to check if there is ground above the player.
    // We know if the terrain is walkable if: a) it's a collider; and b) has tag 'Walkable'.
    // If it, indeed, is a terrain, the player is teleported exactly where the raycast hit ± half of the player's height...
    // this is made in order to make sure the player is not teleported inside of the collider.
    /// </summary>
    void CheckForWalkableTerrainAbove()
    {
        Vector2 raycastOrigin = transform.position;
        raycastOrigin.y += (playerHeight / 2) + (playerHeight / 1000); //minus instead of plus when reversed
        RaycastHit2D hit;
        hit = Physics2D.Raycast(raycastOrigin, Vector2.up, raycastLength);

        if (hit.collider != null && hit.collider.CompareTag("Walkable"))
        {
            Vector2 impactPoint = hit.point;
            Vector2 newPosition = new Vector2(impactPoint.x, impactPoint.y - playerHeight / 2); //plus instead of minus when reversed
            transform.position = newPosition;
            ChangePlayerGravityScale();
        }
    }

    void ChangePlayerGravityScale()
    {
        rigidBody2D.gravityScale *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityTrigger")
        {
            GravityTrigger triggerScript = collision.gameObject.GetComponent<GravityTrigger>();

            if (triggerScript != null)
            {
                float newAngleDegree = triggerScript.GetGravityChangeDegree();
                SetGravityDirection(newAngleDegree);
                SetRotation(newAngleDegree);
            }
        }
    }

    private void SetGravityDirection(float angle)
    {
        movementAngleInDegrees = angle;
        float radians = (angle - 90f) * Mathf.Deg2Rad;
        Vector2 gravityDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        Physics2D.gravity = gravityDirection * Physics2D.gravity.magnitude;
    }

    protected void SetRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnDrawGizmos()
    {
        Vector2 raycastOrigin = transform.position + Vector3.up * (playerHeight / 2 + (playerHeight / 1000));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + Vector2.up * raycastLength);
    }
}
