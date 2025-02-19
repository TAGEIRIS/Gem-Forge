using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public KuManager kuManager;
    //待交付的宝石
    public string Item1;
    public int Num1;
    public string Item2;
    public int Num2;
    //获得的金钱
    public int mNum;

    private void Awake()
    {
        kuManager = GameObject.Find("KuManager").GetComponent<KuManager>();
    }

}
