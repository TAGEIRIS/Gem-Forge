using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnEquip : MonoBehaviour
{
    public Button Button;
    public EquipmentManagerInBag equipmentManagerInBag;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();
    }

    public void takeoff(int Number)
    {
        if(equipmentManagerInBag!=null)equipmentManagerInBag.UnequipItem(Number);
    }
}
