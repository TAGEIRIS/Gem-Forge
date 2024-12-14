using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory Ku;
    public Text itemInformation;

    //预设位置
    public Transform[] equipmentPositions;
    //字典记录当前已存储的装备及其位置索引
    public Dictionary<int,GameObject> equippedItems=new 
        Dictionary<int,GameObject>();

    public void AddSlotToList(Item item)
    {
        if(!Ku.itemList.Contains(item))
        {
            Ku.itemList.Add(item);
            if(item.itemNumber==0)item.itemNumber++;
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
                new Vector3 (0,0,0), equipmentPositions[index].rotation);
            equippedItem.transform.SetParent(equipmentPositions[index], false);
            equippedItem.transform.localScale = new Vector3(1,1,1);
            //将装备与位置关联
            equippedItems[index] = equippedItem;

            //减少物品数量
            slot.slotItem.itemNumber--;
            slot.UpdateSlot();
        }
    }

    public void UnequipItem(int index)
    {
        if (equippedItems.ContainsKey(index))
        {
            string s = equippedItems[index].name;
            s=s.TrimEnd("(Clone)");
            foreach (Item item in Ku.itemList)
            {
                if (item.item.name == s)
                {
                    item.itemNumber++;
                    GameObject gameObject=GameObject.Find(item.name);
                    Slot slot=gameObject.GetComponent<Slot>();
                    slot.UpdateSlot();
                    break;
                }
            }
            Destroy(equippedItems[index]);
            equippedItems.Remove(index);
        }
    }
}
