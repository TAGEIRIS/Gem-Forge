using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private TextMeshProUGUI text;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = new Color(r: 88, g: 155, b: 193);
        text.color = Color.yellow;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = new Color(r: 230, g: 140, b: 210);
        text.color = Color.white;
    }
}
