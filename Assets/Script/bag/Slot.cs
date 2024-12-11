using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public InventoryManager inventoryManager;

    public void UpdateSlot()
    {
        if(slotItem != null&&slotItem.itemNumber<=0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        slotNum.text = slotItem.itemNumber.ToString();
    }

    private void OnEnable()
    {
        UpdateSlot();
    }

    public void OnItemNumChanged()
    {
        UpdateSlot();
    }

    public void AddSlot()
    {
        slotItem.itemNumber++;
        inventoryManager.AddSlotToList(slotItem);
        UpdateSlot();
    }

    public void SubSlot()
    {
        slotItem.itemNumber--;
        inventoryManager.CheckEmpty(slotItem);
        UpdateSlot();
    }
}
