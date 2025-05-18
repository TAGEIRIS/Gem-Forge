using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private Text MoneyShow;
    public KuManager kuManager;
    //出售按钮及其本体
    public Button SellingButton;
    public GameObject SellingBody;
    //购买按钮
    public Button BuyingButton;
    public GameObject BuyingBody;
    //当前选中的物品
    public Slot slot;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("KuManager");
        kuManager = gameObject.GetComponent<KuManager>();

        GameObject gameObject1 = GameObject.Find("Money");
        MoneyShow = gameObject1.GetComponent<Text>(); 

        SellingBody = GameObject.Find("SellingButton");
        SellingButton = SellingBody.GetComponent<Button>();

        BuyingBody = GameObject.Find("BuyingButton");
        BuyingButton = BuyingBody.GetComponent<Button>();
    }
    private void Start()
    {
        UpdateMoney();

        SellingButton.onClick.AddListener(() => sell(slot, 1));
        BuyingButton.onClick.AddListener(()=>Buy(slot, 1));
    }

    public void updatesellnbuy(Slot Gem)
    {
        Debug.Log(Gem.name);
        if (SellingBody == null) return;
        if(BuyingBody == null) return; 
        SellingBody.SetActive(true);
        BuyingBody.SetActive(true);
        if(Gem == null)
        {
            SellingBody.SetActive(false);
            BuyingBody.SetActive(false) ;
        }
        if (Gem.slotItem.itemNumber < 1)
        {
            Debug.Log("物品数量不足，不予出售权限");
            SellingBody.SetActive(false);
        }
        if (kuManager.GemsKu.MoneyNumber < Gem.slotItem.BuyingPrice)
        {
            BuyingBody.SetActive(false);
        }
        if(Gem.slotItem.isLocked==true)
        {
            BuyingBody.SetActive(false);
        }
    }

    //出售宝石
    public void sell(Slot Gem,int number)
    {
        if (Gem == null) return;
        addMoney(Gem.slotItem.SellingPrice *number);
        kuManager.SubtractNumber(Gem.slotItem.GemName, number);
        Gem.UpdateSlot();
    }

    //购入宝石
    public void Buy(Slot Gem,int number)
    {
        if(Gem == null) return;
        subMoney(Gem.slotItem.BuyingPrice *number);
        kuManager.AddNumber(Gem.slotItem.GemName,number);
        Gem.UpdateSlot();
    }

    //对钱的修改
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
