using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReLoad : MonoBehaviour
{
    EquipmentManagerInBag equipmentManagerInBag;
    LevelController levelController;

    public Button ToPlayButton;
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
    }
    public void ToPlay()
    {
        levelController.NextLevel();
    }
    private void OnEnable()
    {
        if(equipmentManagerInBag!=null)equipmentManagerInBag.Awake();
    }
}
