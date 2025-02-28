using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private Text MoneyShow;
    public KuManager kuManager;
    //出售按钮
    public Button SellingButton;
    //当前选中的物品
    public Slot slot;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("KuManager");
        kuManager = gameObject.GetComponent<KuManager>();

        GameObject gameObject1 = GameObject.Find("Money");
        MoneyShow = gameObject1.GetComponent<Text>(); 

        GameObject gameObject2 = GameObject.Find("SellingButton");
        SellingButton = gameObject2.GetComponent<Button>();
    }
    private void Start()
    {
        UpdateMoney();

        SellingButton.onClick.AddListener(() => sell(slot, 1));
    }
    public void sell(Slot Gem,int number)
    {
        if (Gem == null) return;
        if (Gem.slotItem.itemNumber < number) { return; }
        addMoney(Gem.slotItem.SellingPrice *number);
        kuManager.SubtractNumber(Gem.slotItem.GemName, number);
        Gem.UpdateSlot();
    }
    public void addMoney(int money)
    {
        kuManager.GemsKu.MoneyNumber += money;
        UpdateMoney();
    }
    public void subMoney(int money)
    {
        kuManager.GemsKu.MoneyNumber -= money;
        UpdateMoney();
    }

    public void setMoney(int money) 
    {
        kuManager.GemsKu.MoneyNumber = Mathf.Clamp(money, 0, 99999);
        UpdateMoney();
    }

    private void UpdateMoney()
    {
        if(MoneyShow == null)
        {
            Debug.Log("Fuck");
            return;
        }
        MoneyShow.text = "拥有:\n"+kuManager.GemsKu.MoneyNumber;
    }
}
