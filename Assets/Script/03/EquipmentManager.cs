using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //预设的四个位置
    public Transform[] equipmentPosition;
    //应用Inventory，访问宝石列表
    public Inventory Inventory;
    //字典，存储目前已装备宝石以及他们的位置索引
    private Dictionary<int,List<GameObject>>equippedItem=
        new Dictionary<int,List<GameObject>>();
    
    public void AddSlotToList(Item item)    {


    }


}
