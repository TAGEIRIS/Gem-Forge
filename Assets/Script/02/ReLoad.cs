using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReLoad : MonoBehaviour
{
    EquipmentManagerInBag equipmentManagerInBag;
    private void Awake()
    {
        if(equipmentManagerInBag == null)
        {
            GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
            equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();
        }

    }

    private void OnEnable()
    {
        if(equipmentManagerInBag!=null)equipmentManagerInBag.Awake();
    }
}
