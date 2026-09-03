using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipmentSlotUI : MonoBehaviour, IPointerEnterHandler
{
    public string gemId = null;
    private Image image;
    private Button button;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        button.onClick.AddListener(()=>SlotManager.Instance.unequipGem(gemId));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gemId != null)
        SlotManager.Instance.UpdateDetailTextUI(gemId);
    }

    public void UpdateDisplay(string s,Sprite sprite)
    {
        gemId = s;
        image.sprite = sprite;
    }
}
