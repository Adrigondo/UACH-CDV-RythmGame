using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerFloat : MonoBehaviour
{
    #region "Fields"
    [SerializeField]
    [Tooltip("Time in seconds during wich the player is able to float. It starts since the first float in the current fall is activated.")]
    protected float TimeLimit = 5;

    /// <summary>
    /// Main class of every character
    protected NewPlayerBehavior Behaviour;
    protected bool CanFloat = true;

    protected Coroutine TimerRoutine;

    #endregion

    #region "LifeCycle methods"
    void Start()
    {
        Behaviour = GetComponent<NewPlayerBehavior>();
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walkable")
        {
            CanFloat = true;
            if (TimerRoutine != null){
                StopCoroutine(TimerRoutine);
                TimerRoutine=null;
            }
        }
    }
    #endregion

    #region "Public methods"
    public void OnFloat(InputAction.CallbackContext context)
    {
        if (Behaviour.IsPointerOverUI&&context.started)
        {
            if (!Behaviour.IsGrounded)
            {
                Float();
            }
        }
        if (context.performed)
        {
            StopFloat();
        }
    }
    #endregion

    protected void Float()
    {
        if(CanFloat){
            Behaviour.RigidBody2D.gravityScale = 0;
            Behaviour.RigidBody2D.velocity = Vector2.zero;
            if(TimerRoutine==null) TimerRoutine = StartCoroutine(RunTimer());
        }
    }

    protected void StopFloat()
    {
        Behaviour.ResetGravityScale();
    }
    IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(TimeLimit);
        StopFloat();
        CanFloat=false;
    }
}