using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    EquipmentManagerInBag equipmentManagerInBag;


    private void Awake()
    {
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();

    }
    public void ToMain()
    {
        equipmentManagerInBag.ReadyForBattle();
        equipmentManagerInBag.UnEquipAll();
        SceneManager.LoadScene(0);
    }
}
