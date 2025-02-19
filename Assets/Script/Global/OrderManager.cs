using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public TextMeshPro MoneyNumber;
    public KuManager kuManager;



    private void Awake()
    {
        MoneyNumber = GameObject.Find("MoneyNumber").GetComponent<TextMeshPro>();
        kuManager = GameObject.Find("KuManager").GetComponent<KuManager>();

    }

    private void Start()
    {
        
    }

    public void UpdateMoneyNum()
    {
        MoneyNumber.text = kuManager.Money.ToString();
    }

}
