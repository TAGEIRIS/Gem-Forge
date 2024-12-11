using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory mgBag;
    public Text itemInformation;

    //预设位置
    public Transform[] equipmentPositions;
    //字典记录当前已存储的装备及其位置索引
    public Dictionary<int,GameObject> equippedItems=new 
        Dictionary<int,GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void AddSlotToList(Item item)
    {
        if(!mgBag.itemList.Contains(item))
        {
            mgBag.itemList.Add(item);
        }
    }

    public void CheckEmpty(Item item)
    {
        if (item.itemNumber<=0)
        {
            mgBag.itemList.Remove(item);
        }
    }

    public void EquipItem(Item item,int index,Slot slot)
    {
        if(item!=null&&index>=0&&index<equipmentPositions.Length)
        {
            //检查是否已有装备
            if(!equippedItems.ContainsKey(index))
            {
                UnequipItem(index);
            }

            //实例化装备，并放置到预设位置
            GameObject equippedItem = Instantiate(item.item,
                equipmentPositions[index].position, equipmentPositions[index].rotation);
            equippedItem.transform.SetParent(equipmentPositions[index], false);
            equippedItem.transform.localScale = new Vector3(100,100,1);
            //将装备与位置关联
            equippedItems[index] = equippedItem;

            //减少物品数量
            slot.slotItem.itemNumber--;
            slot.UpdateSlot();
            
            CheckEmpty(item);
        }
    }

    public void UnequipItem(int index)
    {
        if (equippedItems.ContainsKey(index))
        {
            Destroy(equippedItems[index]);
            equippedItems.Remove(index);
        }
    }
}
