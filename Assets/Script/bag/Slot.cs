using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //物品信息
    public Item slotItem;
    //UI上的显示
    public Image slotImage;
    //物品个数
    public Text slotNum;
    //装备管理器
    public InventoryManager inventoryManager;
    //下一件装备应该放的位置
    public int slotIndex;

    private void Awake()
    {
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
    }

    //更新slot显示
    public void UpdateSlot()
    {
        //为零则隐藏Slot
        if (slotItem != null && slotItem.itemNumber > 0)
        {
            slotImage.enabled = true;
            slotNum.text = slotItem.itemNumber.ToString();
            slotNum.enabled = true;
        }
        else
        {
            slotImage.enabled = false;
            slotNum.enabled = false;
        }

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
        if (slotItem.itemNumber < slotItem.itemNumberMax)
        {
            slotItem.itemNumber++;
            inventoryManager.AddSlotToList(slotItem);
            UpdateSlot();
        }
    }

    public void SubSlot()
    {
        if (slotItem.itemNumber > 0)
        {
            slotItem.itemNumber--;
            UpdateSlot();
        }
    }

    public void Weapon()
    {
        if (slotItem != null && slotItem.itemNumber > 0)
        {
            for (int i = 0; i < inventoryManager.equipmentPositions.Length; i++)
            {
                if(!inventoryManager.equippedItems.ContainsKey(i))
                {
                    inventoryManager.EquipItem(slotItem,i,this);
                    break;
                }
            }
        }
    }
}
