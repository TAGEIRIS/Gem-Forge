using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuManager : MonoBehaviour
{
    // 宝石库
    public Inventory GemsKu;
    public static KuManager Instance;

    // 字典用于存储宝石的引用
    private Dictionary<string, Item> gemDictionary = new Dictionary<string, Item>();

    public event Action OnMoneyChanged;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

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
        gemDictionary["AB1"].itemNumber = PlayerPrefs.GetInt("AB1" + "的当前数量", 4);
        gemDictionary["AB2"].itemNumber = PlayerPrefs.GetInt("AB2" + "的当前数量", 4);
        gemDictionary["AW1"].itemNumber = PlayerPrefs.GetInt("AW1" + "的当前数量", 4);
        gemDictionary["AW2"].itemNumber = PlayerPrefs.GetInt("AW2" + "的当前数量", 4);
        gemDictionary["BB1"].itemNumber = PlayerPrefs.GetInt("BB1" + "的当前数量", 0);

        GemsKu.MoneyNumber = PlayerPrefs.GetInt("钱",0);
    }

    // 对物品数量的修改
    public void SetNumber(string itemKey, int num)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            gemDictionary[itemKey].itemNumber = Mathf.Clamp(num, 0, gemDictionary[itemKey].itemNumberMax);
            ValidateItemNumbers(itemKey);
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
            gemDictionary[itemKey].itemNumber = Mathf.Clamp
                (gemDictionary[itemKey].itemNumber, 0, gemDictionary[itemKey].itemNumberMax);
            newPlayerPrefs(itemKey,GetItemNumber(itemKey));

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

    public void newPlayerPrefs(string itemKey,int num)
    {
        if (gemDictionary.ContainsKey(itemKey))
        {
            PlayerPrefs.SetInt(itemKey + "的当前数量", num);
        }
    }
}