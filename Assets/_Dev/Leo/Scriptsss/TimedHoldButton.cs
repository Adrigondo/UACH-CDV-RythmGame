using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimedHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Tooltip("SceneChanger gameobject of the current scene.")]
    protected GameObject SceneChanger;

    [SerializeField]
    [Tooltip("The time the button must be hold pressed to complete the action.")]
    protected float SuccessTime;

    protected float CurrentHoldTime = 0f;
    
    protected bool IsPressed = false;


    void Start()
    {
        SceneChanger.SetActive(false);
    }
    void Update()
    {
        if (IsPressed)
        {
            CurrentHoldTime += Time.deltaTime;
            if (CurrentHoldTime >= SuccessTime)
            {
                OnHoldComplete();
                IsPressed = false; // Evita que se llame repetidamente
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        CurrentHoldTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        CurrentHoldTime = 0f;
    }

    private void OnHoldComplete()
    {
        SceneChanger.SetActive(true);
    }
}