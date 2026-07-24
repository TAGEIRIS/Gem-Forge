using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string GemName;
    public GameObject item;
    public Sprite itemImage;
    public int itemNumber;
    public int itemNumberMax=99;
    //出售价格
    public int SellingPrice;
    //购入价格
    public int BuyingPrice;
    //是否锁住(当第一次正式获取到该宝石时解锁)
    public bool isLocked; 
    [TextArea]public string itemInfo;
}
