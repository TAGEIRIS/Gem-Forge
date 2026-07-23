using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public int MoneyNumber;
    public List<Item>itemList=new List<Item>(); 
    public List<string> nameList = new List<string>();
}
