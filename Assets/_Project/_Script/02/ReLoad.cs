using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReLoad : MonoBehaviour
{
    EquipmentManagerInBag equipmentManagerInBag;
    LevelController levelController;

    public GameObject tipwindow;
    private void Awake()
    {
        if(equipmentManagerInBag == null)
        {
            GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
            equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();
        }
        if(levelController == null)
        {
            GameObject gameObject = GameObject.Find("LevelController");
            levelController = gameObject.GetComponent<LevelController>();
        }
        tipwindow.SetActive(false);
    }
    public void ToPlay()
    {
        if(equipmentManagerInBag.equippedItems.Count <4&&!tipwindow.activeSelf)
        {
            tipwindow.SetActive(true);
        }
        else levelController.NextLevel();
    }
    private void OnEnable()
    {
        if(equipmentManagerInBag!=null)equipmentManagerInBag.Awake();
    }
}
