using UnityEngine;
using UnityEngine.InputSystem;

namespace Stariluz
{
    public class PlayerFloat:MonoBehaviour
    {
        #region "Fields"
        [SerializeField]
        [Tooltip("Time in seconds during wich the player is able to float. It starts since the first float in the current fall is activated.")]
        protected float TimeLimit;

        /// <summary>
        /// Main class of every character
        protected NewPlayerBehavior Behaviour;
        #endregion

        #region "LifeCycle methods"
        void Start()
        {
            Behaviour = GetComponent<NewPlayerBehavior>();
        }
        #endregion
        #region "Public methods"
        public void OnFloat(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (!Behaviour.IsGrounded)
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
        #endregion

        protected void Float()
        {
            Behaviour.RigidBody2D.gravityScale = 0;
            Behaviour.RigidBody2D.velocity = Vector2.zero;
        }

        protected void StopFloat()
        {
            Behaviour.ResetGravityScale();
        }

    }
}