// TODO: Lock the teleport/gravity flip mechanics so they are only usable when grounded.
// TODO: Make sure the player interacts correctly with slopes.
// TODO: Lock the teleport/gravity flip mechanics so they are only usable when grounded.
// TODO: Calibrate the gravity and all other parameters relating to physics for a better and smoother game experience.
// TODO: See if there is a way to better optimize all booleans used.

// TODO: Is the float only meant to happen after switching gravity manually? Or will we allow it when in the middle of the air even if no player input happened before?... huh

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class NewPlayerBehavior : MonoBehaviour
{
    #region "Fields"
    [SerializeField] protected bool startLevelWithGravityFlipped = false;
    [SerializeField] protected bool startLevelMovingLeft = false;
    [SerializeField] protected float raycastLength = 15f;
    [SerializeField] protected float gravityScale = 5f;
    [SerializeField] protected float movementAngleInDegrees = 0;
    [SerializeField] protected CameraBehavior cameraBehavior;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected SceneChanger sceneChanger;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private StartPosition _startPositionScript;
    protected float movementAngleInRadians = 0;
    protected float playerHeight;
    protected bool shouldMoveLeft = false;
    protected bool canFloat;
    protected float buttonHeldDownTime;
    protected bool isButtonHeldDown;
    protected Collider2D playerCollider;
    Stariluz.InputPlayer controls;
    private bool _isGravityInverted = false;
    private bool _isGrounded;
    private Rigidbody2D _rigidBody2D;
    #endregion

    #region "Properties"
    public bool IsGrounded
    {
        get { return _isGrounded; }
    }
    public bool IsGravityInverted
    {
        get { return _isGravityInverted; }
    }
    public float MovementSpeed
    {
        get { return _movementSpeed; }
        // set
        // {
        //     if (value != null)
        //     {
        //         _rigidBody2D = value;
        //     }
        // }
    }
    public Rigidbody2D RigidBody2D
    {
        get { return _rigidBody2D; }
        // set
        // {
        //     if (value != null)
        //     {
        //         _rigidBody2D = value;
        //     }
        // }
    }
    public StartPosition StartPositionScript
    {
        get { return _startPositionScript; }
        // set
        // {
        //     if (value != null)
        //     {
        //         _rigidBody2D = value;
        //     }
        // }
    }
    #endregion

    #region "LifeCycle methods"
    protected void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        if (playerCollider != null)
        {
            playerHeight = playerCollider.bounds.size.y;
        }
        else
        {
            Debug.LogError("No Collider2D found on the player");
        }

        if (startLevelWithGravityFlipped)
            _isGravityInverted = true;
        if (startLevelMovingLeft)
            shouldMoveLeft = true;

        RespawnPlayer();
    }

    protected void Update()
    {
        movementAngleInRadians = movementAngleInDegrees * Mathf.Deg2Rad;
        Vector2 movement = new Vector2(Mathf.Cos(movementAngleInRadians), Mathf.Sin(movementAngleInRadians));
        if (shouldMoveLeft)
            movement.x *= -1;

        transform.Translate(movement * _movementSpeed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FinishTrigger")
        {
            FinishLevel();
        }
        /* if (collision.gameObject.tag == "GravityTrigger")
        {
            GravityTrigger triggerScript = collision.gameObject.GetComponent<GravityTrigger>();

            if (triggerScript != null)
            {
                float newAngleDegree = triggerScript.GetGravityChangeDegree();
                SetGravityDirection(newAngleDegree);
                SetRotation(newAngleDegree);
            }
        } */
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            _isGrounded = true;
        }
        if (collision.gameObject.tag == "Hazard")
        {
            Death();
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            _isGrounded = false;
        }
    }

    protected void OnDrawGizmos()
    {
        Vector2 raycastOrigin = transform.position;
        raycastOrigin.y += (playerHeight / 2) + (playerHeight / 1000);
        Vector2 upDirection = transform.TransformDirection(Vector2.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin, raycastOrigin + upDirection * raycastLength);
    }
    #endregion

    #region  "Public methods"
    private bool canSlide = true;
    public void OnChangeGravity(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log((context.action.activeControl.device, context.action.activeControl.name, context.action.activeControl.displayName));
            if (context.action.activeControl.device is Touchscreen)
            {
                if (context.action.activeControl.name == "up")
                {
                    if (!IsGravityInverted && canSlide)
                    {
                        canSlide = false;
                        CheckGravityChange();
                    }

                }
                else if (context.action.activeControl.name == "down")
                {
                    if (IsGravityInverted && canSlide)
                    {
                        canSlide = false;
                        CheckGravityChange();

                    }
                }

            }
            else
            {
                CheckGravityChange();
            }
        }
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CheckTeleport();
        }
    }

    public void OnSlideEnd(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canSlide = true;
        }
    }

    public void ResetGravityScale()
    {
        if (!IsGravityInverted)
            RigidBody2D.gravityScale = gravityScale;
        else
            RigidBody2D.gravityScale = -gravityScale;
    }
    #endregion

    #region "Protected methods"
    protected void RespawnPlayer()
    {
        transform.position = _startPositionScript.GetStartPosition();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();

    }
    protected void ChangePlayerGravityScale()
    {
        _isGravityInverted = !_isGravityInverted;
        _rigidBody2D.gravityScale *= -1;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180);

        shouldMoveLeft = _isGravityInverted ? true : false;
    }

    protected void CheckGravityChange()
    {
        if (_isGrounded)
            ChangePlayerGravityScale();
    }

    protected void CheckTeleport()
    {
        if (_isGrounded)
            CheckForWalkableTerrainAbove();
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
            if (hit.collider != null && hit.collider.CompareTag("Walkable") && _isGrounded)
            {
                Teleport(hit);
                break;
            }
        }
    }

    protected void Teleport(RaycastHit2D hit)
    {
        Vector2 impactPoint = hit.point;
        Vector2 newPosition = new Vector2(impactPoint.x, impactPoint.y + (_isGravityInverted ? 1 : -1) * playerHeight / 2);
        transform.position = newPosition;
        _rigidBody2D.velocity = Vector2.zero;
        ChangePlayerGravityScale();
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

    public void Death()
    {
        // Debug.LogAssertion("Death");
        RespawnPlayer();

        if (_isGravityInverted)
        {
            ChangePlayerGravityScale();
        }

        if (cameraBehavior != null)
        {
            cameraBehavior.RestartCamera();
        }
        else
        {
            Debug.LogError("No Camera Behavior script found");
        }

        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No Audio Source script found");
        }
    }

    protected void FinishLevel()
    {
        sceneChanger.ChangeSceneWithCode("LevelsMenuV3");
    }
    #endregion
}
