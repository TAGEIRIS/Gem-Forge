using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManagerInBag : MonoBehaviour
{
    static EquipmentManagerInBag instance;
    public Inventory Ku;
    public Text itemInformation;

    //预设位置
    public Transform[] equipmentPositions;
    //字典记录当前已存储的装备及其位置索引
    public Dictionary<int,GameObject> equippedItems=new 
        Dictionary<int,GameObject>();

    public void Awake()
    {
        GameObject gameObject = GameObject.Find("NowWeapon");
        if (gameObject == null) return;

        // 获取所有子对象的Transform组件
        Transform[] allTransforms = gameObject.GetComponentsInChildren<Transform>();
        List<Transform> equipmentPositionsList = new List<Transform>(allTransforms);

        // 移除自身，不添加到数组中
        equipmentPositionsList.Remove(gameObject.transform);

        // 将List转换回数组
        equipmentPositions = equipmentPositionsList.ToArray();
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
            AddKuNumber(s);
            Destroy(equippedItems[index]);
            equippedItems.Remove(index);
        }
    }

    //增加库中对应物品的数量并更新
    public void AddKuNumber(string s)
    {
        foreach (Item item in Ku.itemList)
        {
            if (item.item.name == s)
            {
                item.itemNumber++;
                GameObject gameObject = GameObject.Find(item.name);
                if (gameObject == null) break;
                Slot slot = gameObject.GetComponent<Slot>();
                slot.UpdateSlot();
                break;
            }
        }
    }

    public void UnEquipAll()
    {
        UnequipItem(0);
        UnequipItem(1);
        UnequipItem(2);
        UnequipItem(3);
        equippedItems.Clear();
    }

    public void ReadyForBattle()
    {
        foreach (GameObject gameObject in equippedItems.Values)
        {
            if(gameObject!=null)
            {
                string s=gameObject.name;
                s = s.TrimEnd("(Clone)");
                Ku.nameList.Add(s);
            }
        }
    }
    public void UnReadyForBattle()
    {
        Ku.nameList.Clear();
    }
    private void OnDestroy()
    {
        UnReadyForBattle();
    }
}
