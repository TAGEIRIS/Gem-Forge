using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnEquip : MonoBehaviour
{
    public Button Button;
    public int ButtonNumber;
    public InventoryManager inventoryManager;

    private void OnEnable()
    {
        GameObject gameObject = GameObject.Find("BagCanvas");
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }
    private void Update()
    {
        Button.onClick.AddListener(call: () =>
        {
            takeoff();
        });
    }
    private void OnDestroy()
    {
        takeoff() ;
    }

    private void takeoff()
    {
        inventoryManager.UnequipItem(ButtonNumber);
    }
}
