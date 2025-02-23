using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuManager : MonoBehaviour
{
    //宝石库
    public Inventory GemsKu;
    //所有宝石
    public Item AB1;//Blue
    public Item AB2;//Green
    public Item AW1;//Red
    public Item AW2;//Yellow
    public Item BB1;//Crystal

    private void Awake()
    {
        AB1 = FindGem(AB1, "AB1");
        AB2 = FindGem(AB2, "AB2");
        AW1 = FindGem(AW1, "AW1");
        AW2 = FindGem(AW2, "AW2");
        BB1 = FindGem(BB1, "BB1");




        ReSetKu();
    }
    public void ReSetKu()
    {
        AB1.itemNumber = 4;
        AB2.itemNumber = 4;
        AW1.itemNumber = 4;
        AW2.itemNumber = 4;
        BB1.itemNumber = 0;
    }

    //对物品数量的修改
    public void SetNumber(string item, int num)
    {
        if (item == "BB1")
        {
            BB1.itemNumber = Mathf.Clamp(num, 0, BB1.itemNumberMax);
        }
        else if (item == "AB1")
        {
            AB1.itemNumber = Mathf.Clamp(num, 0, AB1.itemNumberMax);
        }
        else if (item == "AB2")
        {
            AB2.itemNumber = Mathf.Clamp(num, 0, AB2.itemNumberMax);
        }
        else if (item == "AW1")
        {
            AW1.itemNumber = Mathf.Clamp(num, 0, AW1.itemNumberMax);
        }
        else if (item == "AW2")
        {
            AW2.itemNumber = Mathf.Clamp(num, 0, AW2.itemNumberMax);
        }
    }
    public void AddNumber(string item, int num)
    {
        if (item == "BB1")
        {
            BB1.itemNumber += num;
        }
        else if (item == "AB1")
        {
            AB1.itemNumber += num;
        }
        else if (item == "AB2")
        {
            AB2.itemNumber += num;
        }
        else if (item == "AW1")
        {
            AW1.itemNumber += num;
        }
        else if (item == "AW2")
        {
            AW2.itemNumber += num;
        }
        ValidateItemNumbers();
    }
    public void SubtractNumber(string item, int num)
    {

        if (item == "BB1")
        {
            BB1.itemNumber -= num;
        }
        else if (item == "AB1")
        {
            AB1.itemNumber -= num;
        }
        else if (item == "AB2")
        {
            AB2.itemNumber -= num;
        }
        else if (item == "AW1")
        {
            AW1.itemNumber -= num;
        }
        else if (item == "AW2")
        {
            AW2.itemNumber -= num;
        }
        ValidateItemNumbers();
    }

    public void Selling(string item, int num)
    {

    }
    private void ValidateItemNumbers()
    {
        BB1.itemNumber = Mathf.Clamp(BB1.itemNumber, 0, BB1.itemNumberMax);
        AB1.itemNumber = Mathf.Clamp(AB1.itemNumber, 0, AB1.itemNumberMax);
        AB2.itemNumber = Mathf.Clamp(AB2.itemNumber, 0, AB2.itemNumberMax);
        AW1.itemNumber = Mathf.Clamp(AW1.itemNumber, 0, AW1.itemNumberMax);
        AW2.itemNumber = Mathf.Clamp(AW2.itemNumber, 0, AW2.itemNumberMax);
    }

    //初始化找到宝石item
    private Item FindGem(Item item, string s)
    {
        foreach (var itema in GemsKu.itemList)
        {
            if (itema.name == s)
            {
                return itema;
            }
        }
        Debug.Log("Fuck");
        return null;
    }
}