using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReLoad : MonoBehaviour
{
    InventoryManager inventoryManager;
    private void Awake()
    {
        if(inventoryManager == null)
        {
            GameObject gameObject = GameObject.Find("InventoryManager");
            inventoryManager = gameObject.GetComponent<InventoryManager>();
        }

    }

    private void OnEnable()
    {
        if(inventoryManager!=null)inventoryManager.Awake();
    }
}
