using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnEquip : MonoBehaviour
{
    public Button Button;
    public InventoryManager inventoryManager;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("InventoryManager");
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    public void takeoff(int Number)
    {
        if(inventoryManager!=null)inventoryManager.UnequipItem(Number);
    }
}
