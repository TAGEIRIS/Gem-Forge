using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuManager : MonoBehaviour
{
    // 宝石库
    public Inventory GemsKu;

    // 字典用于存储宝石的引用
    private Dictionary<string, Item> gemDictionary = new Dictionary<string, Item>();

    private void Awake()
    {
        // 初始化字典
        gemDictionary["AB1"] = FindGem("AB1");
        gemDictionary["AB2"] = FindGem("AB2");
        gemDictionary["AW1"] = FindGem("AW1");
        gemDictionary["AW2"] = FindGem("AW2");
        gemDictionary["BB1"] = FindGem("BB1");

        ReSetKu();
    }

    public void ReSetKu()
    {
        gemDictionary["AB1"].itemNumber = 4;
        gemDictionary["AB2"].itemNumber = 4;
        gemDictionary["AW1"].itemNumber = 4;
        gemDictionary["AW2"].itemNumber = 4;
        gemDictionary["BB1"].itemNumber = 0;

        GemsKu.MoneyNumber = 0;
    }

    // 对物品数量的修改
    public void SetNumber(string itemKey, int num)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            gemDictionary[itemKey].itemNumber = Mathf.Clamp(num, 0, gemDictionary[itemKey].itemNumberMax);
        }
    }

    public void AddNumber(string itemKey, int num)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            gemDictionary[itemKey].itemNumber += num;
            ValidateItemNumbers(itemKey);
        }
    }

    public void SubtractNumber(string itemKey, int num)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            gemDictionary[itemKey].itemNumber -= num;
            ValidateItemNumbers(itemKey);
        }
    }
    public int GetItemNumber(string itemKey)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            return gemDictionary[itemKey].itemNumber;
        }
        return 0;
    }

    private void ValidateItemNumbers(string itemKey)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            gemDictionary[itemKey].itemNumber = Mathf.Clamp(gemDictionary[itemKey].itemNumber, 0, gemDictionary[itemKey].itemNumberMax);
        }
    }

    // 通过string获取宝石item
    public Item FindGem(string gemName)
    {
        foreach (var item in GemsKu.itemList)
        {
            if (item.name == gemName)
            {
                return item;
            }
        }
        Debug.LogError($"Gem not found: {gemName}");
        return null;
    }

}