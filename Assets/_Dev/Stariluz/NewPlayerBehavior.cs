// TODO: Lock the teleport/gravity flip mechanics so they are only usable when grounded.
// TODO: WHY DOES THE RAYCAST NOT DETECT THE GROUND WHAT IS GOING ON I'M SCARED.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Stariluz
{
    public class NewPlayerBehavior : MonoBehaviour, InputPlayer.IGameplayActions
    {
        #region "Properties"
        [SerializeField] protected bool startLevelWithGravityFlipped = false;
        [SerializeField] protected bool startLevelMovingLeft = false;
        [SerializeField] protected float raycastLength = 15f;
        [SerializeField] protected float gravityScale = 9.81f;
        [SerializeField] protected float movementSpeed = 5f;
        [SerializeField] protected float movementAngleInDegrees = 0;
        protected float movementAngleInRadians = 0;
        protected float playerHeight;
        protected bool hasGravityBeenFlipped = false;
        protected bool shouldMoveLeft = false;
        protected bool isGrounded;
        protected bool canFloat;
        [SerializeField] protected float buttonHoldThreshold = 0.15f;
        protected float buttonHeldDownTime;
        protected bool isButtonHeldDown;
        protected Collider2D playerCollider;
        protected Rigidbody2D rigidBody2D;
        InputPlayer controls;
        #endregion


        #region "LifeCycle functions"

        public void OnEnable()
        {
            if (controls == null)
            {
                controls = new InputPlayer();
                // Tell the "gameplay" action map that we want to get told about
                // when actions get triggered.
                controls.Gameplay.SetCallbacks(this);
            }
            controls.Gameplay.Enable();
        }

        public void OnDisable()
        {
            controls.Gameplay.Disable();
        }
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

            if (startLevelWithGravityFlipped)
                hasGravityBeenFlipped = true;
            if (startLevelMovingLeft)
                shouldMoveLeft = true;
        }

        protected void Update()
        {
            movementAngleInRadians = movementAngleInDegrees * Mathf.Deg2Rad;
            Vector2 movement = new Vector2(Mathf.Cos(movementAngleInRadians), Mathf.Sin(movementAngleInRadians));
            if (shouldMoveLeft)
                movement.x *= -1;

            transform.Translate(movement * movementSpeed * Time.deltaTime);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
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
                isGrounded = true;
            }
        }

        protected void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Walkable")
            {
                isGrounded = false;
            }
        }
        #endregion

        #region  "Public methods"
        public void OnChangeGravity(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CheckGravityChange();
            }
        }

        public void OnFloat(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!isGrounded)
                {
                    Float();
                }
            }
            if (context.performed)
            {
                // if(context.interaction is HoldInteraction){

                //     Float();
                // }
                StopFloat();
            }
        }
        public void OnTeleport(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CheckTeleport();
            }
        }

        #endregion

        #region "Protected methods"
        protected void ChangePlayerGravityScale()
        {
            hasGravityBeenFlipped = !hasGravityBeenFlipped; //
            rigidBody2D.gravityScale *= -1;
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180);

            shouldMoveLeft = hasGravityBeenFlipped ? true : false; //
        }

        protected void Float()
        {
            rigidBody2D.gravityScale = 0;
            rigidBody2D.velocity = Vector2.zero;
        }

        protected void StopFloat()
        {
            if (!hasGravityBeenFlipped)
                rigidBody2D.gravityScale = 5; //CAMBIAR POR VARIABLE DE GRAVITYSCALE QUE SEA SERIALIZABLE O ALGO IDK
            else
                rigidBody2D.gravityScale = -5;
        }

        protected void CheckGravityChange()
        {
            if (isGrounded)
                ChangePlayerGravityScale();
        }

        protected void CheckTeleport()
        {
            if (isGrounded)
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
                if (hit.collider != null && hit.collider.CompareTag("Walkable") && isGrounded)
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
        #endregion
    }

}