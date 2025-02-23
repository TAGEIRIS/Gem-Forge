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
    [TextArea]public string itemInfo;
}
