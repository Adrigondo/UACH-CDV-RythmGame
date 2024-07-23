using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TimedHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    [Tooltip("SceneChanger gameobject of the current scene.")]
    protected GameObject SceneChanger;

    [SerializeField]
    [Tooltip("Image to show for the button.")]
    protected Image ImageFill;

    [SerializeField]
    [Tooltip("The time the button must be hold pressed to complete the action.")]
    protected float SuccessTime;

    protected float CurrentHeldTime = 0f;
    
    protected bool IsOnClick = false;


    void Start()
    {
        SceneChanger.SetActive(false);
        ImageFill.GetComponent<Image>();
        ImageFill.fillAmount = 0;
    }
    void Update()
    {
        if (IsOnClick)
        {
            CurrentHeldTime += Time.deltaTime;
            ImageFill.fillAmount = CurrentHeldTime/SuccessTime;
            if (CurrentHeldTime >= SuccessTime)
            {
                OnHoldComplete();
                IsOnClick = false; // Evita que se llame repetidamente
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsOnClick = true;
        CurrentHeldTime = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsOnClick = false;
        CurrentHeldTime = 0f;
        ImageFill.fillAmount = 0;
    }

    private void OnHoldComplete()
    {
        SceneChanger.SetActive(true);
    }
}