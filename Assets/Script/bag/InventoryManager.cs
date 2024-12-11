using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory mgBag;
    public Text itemInformation;


    private void Awake()
    {
        if (instance == null)
            Destroy(this);
        instance = this;
    }
    public void AddSlotToList(Item item)
    {
        if(!mgBag.itemList.Contains(item))
        {
            mgBag.itemList.Add(item);
        }
    }

    public void CheckEmpty(Item item)
    {
        if (item.itemNumber<=0)
        {
            mgBag.itemList.Remove(item);
        }
    }
}
