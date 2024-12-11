using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{ 
    public GameObject item;
    public string itemName;
    public Sprite itemImage;
    public int itemNumber;
    public int itemNumberMax=99;
    [TextArea]public string itemInfo;
   

}
