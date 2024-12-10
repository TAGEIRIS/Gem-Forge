using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public Item thisItem;
    public Inventory inventory;
    public void AddNewItem()
    {
        if(!inventory.itemList.Contains(thisItem))
        {
            inventory.itemList.Add(thisItem);
        }
        else
        {
            thisItem.itemNumber++;
        }
    }
}
