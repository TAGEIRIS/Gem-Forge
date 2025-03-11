using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceBody : MonoBehaviour
{
    // 至多三个原料
    public string RM1name;
    public int RM1num;
    public string RM2name;
    public int RM2num;
    public string RM3name;
    public int RM3num;
    // 至多2个产品
    public string Product1name;
    public int Product1Num;
    public string Product2name;
    public int Product2num;
    // 制作周期
    public int time;
    // 宝石库
    public KuManager kuManager;

    // 装置信息展示
    public Image RM1image;
    public Text RM1text;
    public Image RM2image;
    public Text RM2text;
    public Image RM3image;
    public Text RM3text;
    public Image Product1image;
    public Text Product1text;
    public Image Product2image;
    public Text Product2text;

    private void Awake()
    {
        // 获取 KuManager 组件
        GameObject kuManagerObject = GameObject.Find("KuManager");
        if (kuManagerObject != null)
        {
            kuManager = kuManagerObject.GetComponent<KuManager>();
        }
        else
        {
            Debug.LogError("KuManager object not found!");
        }

        // 初始化原料和产品的 UI 组件
        InitializeUIComponents();
    }

    private void InitializeUIComponents()
    {
        // 初始化原料1的 UI
        if (RM1num != 0)
        {
            RM1image = GameObject.Find("RM1Image").GetComponent<Image>();
            RM1text = GameObject.Find("RM1Text").GetComponent<Text>();
            RM1image.sprite = kuManager.FindGem(RM1name).itemImage;
            RM1text.text =RM1num+ " x ";
        }

        // 初始化原料2的 UI
        if (RM2num != 0)
        {
            RM2image = GameObject.Find("RM2Image").GetComponent<Image>();
            RM2text = GameObject.Find("RM2Text").GetComponent<Text>();
            RM2image.sprite = kuManager.FindGem(RM2name).itemImage;
            RM2text.text =RM2num+" x ";
        }

        // 初始化原料3的 UI
        if (RM3num != 0)
        {
            RM3image = GameObject.Find("RM3Image").GetComponent<Image>();
            RM3text = GameObject.Find("RM3Text").GetComponent<Text>();
            RM3image.sprite = kuManager.FindGem(RM3name).itemImage;
            RM3text.text = RM3num + " x ";
        }

        // 初始化产品1的 UI
        if (Product1Num != 0)
        {
            Product1image = GameObject.Find("Product1Image").GetComponent<Image>();
            Product1text = GameObject.Find("Product1Text").GetComponent<Text>();
            Product1image.sprite = kuManager.FindGem(Product1name).itemImage;
            Product1text.text = Product1Num + " x ";
        }

        // 初始化产品2的 UI
        if (Product2num != 0)
        {
            Product2image = GameObject.Find("Product2Image").GetComponent<Image>();
            Product2text = GameObject.Find("Product2Text").GetComponent<Text>();
            Product2image.sprite = kuManager.FindGem(Product2name).itemImage;
            Product2text.text = Product2num + " x ";
        }
    }
}