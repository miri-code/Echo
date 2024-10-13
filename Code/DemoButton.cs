using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DemoButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TMP_Text buttonText;
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color pressedColor = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TMP_Text>(); // Auto-assign the TMP_Text if not manually set
        }
        buttonText.color = normalColor; // Set default color
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            Debug.Log("Hover Start: " + gameObject.name);
            buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
          Debug.Log("Hover End: " + gameObject.name);
    buttonText.color = normalColor;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.color = hoverColor; // Revert back to hover color when button is released
    }
}



