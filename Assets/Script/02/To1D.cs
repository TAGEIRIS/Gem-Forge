using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class To1D : MonoBehaviour
{
    public Button to1DButton;
    EquipmentManagerInBag equipmentManagerInBag;
    private void Awake()
    {
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();

    }
    void Start()
    {
        to1DButton.onClick.AddListener(call: () =>
        {
            equipmentManagerInBag.ReadyForBattle();
            equipmentManagerInBag.UnEquipAll();
            SceneManager.LoadScene("03-GamePlay");
        });
    }

}
