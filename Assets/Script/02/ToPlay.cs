using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToPlay : MonoBehaviour
{
    public Button ToPlayButton;
    EquipmentManagerInBag equipmentManagerInBag;
    private void Awake()
    {
        GameObject gameObject = GameObject.Find("EquipmentManagerInBag");
        equipmentManagerInBag = gameObject.GetComponent<EquipmentManagerInBag>();

        ToPlayButton = GetComponent<Button>();

    }
    void Start()
    {
        ToPlayButton.onClick.AddListener(call: () =>
        {
            Debug.Log("ToPlay");
            equipmentManagerInBag.ReadyForBattle();
            equipmentManagerInBag.UnEquipAll();
            SceneManager.LoadScene("03-GamePlay");
        });
    }

}
