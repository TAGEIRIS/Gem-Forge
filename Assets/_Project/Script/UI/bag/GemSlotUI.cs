using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemSlotUI : MonoBehaviour, IPointerEnterHandler
{
    private Image image;
    private int count;
    private Text text;
    public string gemId;
    private Button button;

    void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        gemId = gameObject.name;
        button = GetComponent<Button>();

        button.onClick.AddListener(() => SlotManager.Instance.equipGem(gemId));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SlotManager.Instance.UpdateDetailTextUI(gemId);
    }

    public void UpdateDisplay(Sprite sprite, int count)
    {
        this.image.sprite = sprite;
        this.count = count;
        string s;
        if (count < 10)
        {
            s = '0' + count.ToString();
        }
        else s = count.ToString();
        this.text.text = s;
    }
}