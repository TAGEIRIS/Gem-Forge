using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private Text MoneyShow;
    private KuManager kuManager;

    private void Awake()
    {
        GameObject gameObject = GameObject.Find("KuManager");
        kuManager = gameObject.GetComponent<KuManager>();

        GameObject gameObject1 = GameObject.Find("Money");
        MoneyShow = gameObject1.GetComponent<Text>(); 

    }
    public void sell(Item Gem,int number)
    {
        addMoney(Gem.SellingPrice*number);
        kuManager.SubtractNumber(Gem.GemName, number);
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
        MoneyShow.text = "с╣сп:"+kuManager.GemsKu.MoneyNumber;
    }
}
