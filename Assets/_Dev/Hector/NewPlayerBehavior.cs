// TODO: Lock the teleport/gravity flip mechanics so they are only usable when grounded.
// TODO: WHY DOES THE RAYCAST NOT DETECT THE GROUND WHAT IS GOING ON I'M SCARED.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerBehavior : MonoBehaviour
{
    [SerializeField] protected float gravityScale = 9.81f;
    [SerializeField] protected float movementAngleInDegrees = 0;
    [SerializeField] protected float movementSpeed = 5f;
    protected float movementAngleInRadians = 0;
    protected float playerHeight;
    [SerializeField] protected float raycastLength = 15f;
    protected Collider2D playerCollider;
    protected Rigidbody2D rigidBody2D;

    protected void Start()
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
        CheckIfGrounded();
    }

    protected void Update()
    {
        /* movementAngleInRadians = movementAngleInDegrees * Mathf.Deg2Rad;
        Vector2 movement = new Vector2(Mathf.Cos(movementAngleInRadians), Mathf.Sin(movementAngleInRadians));
        transform.Translate(movement * movementSpeed * Time.deltaTime); */
        CheckIfGrounded();

        if (Input.GetMouseButtonDown(0))
        {
            CheckForWalkableTerrainAbove();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ChangePlayerGravityScale();
        }
    }

    protected void CheckIfGrounded()
    {
        Vector2 raycastOrigin = transform.position;
        raycastOrigin.y = playerHeight;
        float raycastLength = 0.5f;
        Vector2 downDirection = transform.TransformDirection(Vector2.down);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, downDirection, raycastLength);

        foreach (RaycastHit2D hit in hits)
        {   
            if (hit.collider != null && hit.collider.CompareTag("Walkable"))
            {
                Debug.Log("PLAY IS GROUNDED I REPEAT PLAYER IS GROUNDED!");
            }
            else
            {
                Debug.Log("Player is not grounded!");
            }
        }
    }

    /// <summary>
    // The following function makes use of a RaycastHit2D to check if there is ground above the player.
    // We know if the terrain is walkable if: a) it's a collider; and b) has tag 'Walkable'.
    // If it, indeed, is a terrain, the player is teleported exactly where the raycast hit ± half of the player's height...
    // this is made in order to make sure the player is not teleported inside of the collider.
    // The aforementioned raycast will always point above the player, no matter the angle. As well as checking for every collider hit...
    // until it finds a 'Walkable'. This means it will always stop at the first 'Walkable' found, even if there are more beyond the first one.
    /// </summary>
    protected void CheckForWalkableTerrainAbove()
    {
        Vector2 raycastOrigin = transform.position;
        raycastOrigin.y += (playerHeight / 2) + (playerHeight / 1000);
        Vector2 upDirection = transform.TransformDirection(Vector2.up);
        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, upDirection, raycastLength);

        foreach (RaycastHit2D hit in hits)
        {   
            if (hit.collider != null && hit.collider.CompareTag("Walkable"))
            {
                Teleport(hit);
                break;
            }
        }
    }

    protected void Teleport(RaycastHit2D hit)
    {
        Vector2 impactPoint = hit.point;
        Vector2 newPosition = new Vector2(impactPoint.x, impactPoint.y - playerHeight / 2);
        transform.position = newPosition;
        rigidBody2D.velocity = Vector2.zero;
        ChangePlayerGravityScale();
    }

    protected void ChangePlayerGravityScale()
    {
        rigidBody2D.gravityScale *= -1;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
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

    protected void SetGravityDirection(float angle)
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

    protected void OnDrawGizmos()
    {
        Vector2 raycastOrigin = transform.position;
        raycastOrigin.y += (playerHeight / 2) + (playerHeight / 1000);
        Vector2 upDirection = transform.TransformDirection(Vector2.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + upDirection * raycastLength);

        Vector2 raycastOrigin2 = transform.position;
        float raycastLength2 = playerHeight;
        Vector2 downDirection = transform.TransformDirection(Vector2.down);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(raycastOrigin2, raycastOrigin2 + downDirection * raycastLength2);
    }
}
