using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject SceneChanger;
    public Image progressButtonImage;
    public float holdTime; // Tiempo necesario para mantener el botón presionado
    private float holdTimer = 0f;
    private bool isHolding = false;


    void Start()
    {
        SceneChanger.SetActive(false);
        progressButtonImage.GetComponent<Image>();
        progressButtonImage.fillAmount = 0;
    }
    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            progressButtonImage.fillAmount = holdTimer/holdTime;
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
        progressButtonImage.fillAmount = 0;
    }

    private void OnHoldComplete()
    {
        SceneChanger.SetActive(true);
    }
}