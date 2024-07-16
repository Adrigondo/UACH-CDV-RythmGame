using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SceneChanger;
    public float holdTime; // Tiempo necesario para mantener el botón presionado
    private float holdTimer = 0f;
    private bool isHolding = false;


    void Start()
    {
        SceneChanger.SetActive(false);
    }
    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime)
            {
                OnHoldComplete();
                isHolding = false; // Evita que se llame repetidamente
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        holdTimer = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdTimer = 0f;
    }

    private void OnHoldComplete()
    {
        SceneChanger.SetActive(true);
    }
}