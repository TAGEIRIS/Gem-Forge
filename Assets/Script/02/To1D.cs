using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class To1D : MonoBehaviour
{
    public Button to1DButton;
    InventoryManager inventoryManager;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("InventoryManager");
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }
    void Start()
    {
        to1DButton.onClick.AddListener(call: () =>
        {
            inventoryManager.ReadyForBattle();
            inventoryManager.UnEquipAll();
            SceneManager.LoadScene("03-GamePlay");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
