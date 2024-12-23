using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManagerInPlay : MonoBehaviour
{
    //预设的四个位置
    public Transform[] equipmentPosition;
    //应用Inventory，访问宝石列表
    public Inventory Ku;
    private void Awake()
    {
        GameObject gameObject = GameObject.Find("WeaponPosition");
        if (gameObject == null) return;

        // 获取所有子对象的Transform组件
        Transform[] allTransforms = gameObject.GetComponentsInChildren<Transform>();
        List<Transform> equipmentPositionsList = new List<Transform>(allTransforms);

        // 移除自身，不添加到数组中
        equipmentPositionsList.Remove(gameObject.transform);

        // 将List转换回数组
        equipmentPosition = equipmentPositionsList.ToArray();
    }

    private void OnEnable()
    {
        int i = 0;
        foreach(string equippedItemName in Ku.nameList)
        {
            GameObject gameObject = GameObject.Find(equippedItemName);
            if(gameObject == null) return;
            GameObject clone = Instantiate(gameObject, equipmentPosition[i].position
                , equipmentPosition[i].rotation);
            clone.transform.SetParent(this.transform);
            i++;
        }
    }

}
