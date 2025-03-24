using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceBody : MonoBehaviour
{
    //装置代号
    public int DeviceNumber;

    // 至多三个原料
    public string RM1name;
    public int RM1num;
    public string RM2name;
    public int RM2num;
    public string RM3name;
    public int RM3num;
    // 至多2个产品
    public string Product1name;
    public int Product1num;
    public string Product2name;
    public int Product2num;
    // 制作周期
    public int time;
    // 宝石库
    public KuManager kuManager;
    //关卡管理器
    public LevelController levelController;
    //此前记录的波次(通过差值判断过了几天)
    public int recordedWave;

    // 装置信息展示
    private Image RM1image;
    private Text RM1text;
    private Image RM2image;
    private Text RM2text;
    private Image RM3image;
    private Text RM3text;
    private Image Product1image;
    private Text Product1text;
    private Image Product2image;
    private Text Product2text;
    private Text CostTime;  

    //执行按钮及剩余运作时间(0代表未运作)
    private GameObject ProduceButtonBody;
    private Button Producebutton;
    public int Operationtime;
    private Text Operating;

    private void Awake()
    {
        // 获取 KuManager 和Levelcontroler组件
        kuManager = GameObject.Find("KuManager").GetComponent<KuManager>();
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        

        Operating = GameObject.Find("Operating").GetComponent<Text>();

        //绑定button
        ProduceButtonBody = transform.Find("Producebutton").gameObject;
        Producebutton = ProduceButtonBody.GetComponent<Button>();
        Producebutton.onClick.AddListener(() =>
        {
            OperateDevice();
        });

        // 初始化原料和产品的 UI 组件
        InitializeUIComponents();
        //更新当前时间
        UpdateOperationStatus();
    }

    //初始化UI面板
    private void InitializeUIComponents()
    {
        // 初始化原料1的 UI
        if (RM1num != 0)
        {
            RM1image = transform.Find("RM1/RM1Image").GetComponent<Image>();
            RM1text = transform.Find("RM1/RM1Text").GetComponent<Text>();
            RM1image.sprite = kuManager.FindGem(RM1name).itemImage;
            RM1text.text =RM1num+ " x ";
        }

        // 初始化原料2的 UI
        if (RM2num != 0)
        {
            RM2image = transform.Find("RM2/RM2Image").GetComponent<Image>();
            RM2text = transform.Find("RM2/RM2Text").GetComponent<Text>();
            RM2image.sprite = kuManager.FindGem(RM2name).itemImage;
            RM2text.text =RM2num+" x ";
        }

        // 初始化原料3的 UI
        if (RM3num != 0)
        {
            RM3image = transform.Find("RM3/RM3Image").GetComponent<Image>();
            RM3text = transform.Find("RM3/RM3Text").GetComponent<Text>();
            RM3image.sprite = kuManager.FindGem(RM3name).itemImage;
            RM3text.text = RM3num + " x ";
        }

        // 初始化产品1的 UI
        if (Product1num != 0) 
        {
            Product1image = transform.Find("Product1/Product1Image").GetComponent<Image>();
            Product1text = transform.Find("Product1/Product1Text").GetComponent<Text>();
            Product1image.sprite = kuManager.FindGem(Product1name).itemImage;
            Product1text.text = Product1num + " x ";
        }

        // 初始化产品2的 UI
        if (Product2num != 0)
        {
            Product2image = transform.Find("Product2/Product2Image").GetComponent<Image>();
            Product2text = transform.Find("Product2/Product2Text").GetComponent<Text>();
            Product2image.sprite = kuManager.FindGem(Product2name).itemImage;
            Product2text.text = Product2num + " x ";
        }

        //初始化生产时间
        CostTime = transform.Find("CostTime").GetComponent<Text>();
        CostTime.text = "生产耗时" + time.ToString() + "天";
    }

    //更新运行状态
    public void UpdateOperationStatus()
    {
        if(recordedWave+1==levelController.currenWave)
        {
            Operationtime--;
            recordedWave = levelController.currenWave;
        }

        if (Operationtime > 0)
        {
            ProduceButtonBody.SetActive(false);
            Operating.text = "运行中\n" + "还剩" + Operationtime + "天";
        }
        else if(levelController.currenWave > 1)
        { 
            Operationtime = 0;
            Debug.Log("完成生产");
            if(Product1num != 0) kuManager.AddNumber(Product1name, Product1num);
            if (Product2num != 0) kuManager.AddNumber(Product2name, Product2num);
            ProduceButtonBody.SetActive(true); 
        }
    }

    //开始运行
    public void OperateDevice()
    {
        if (RM1num != 0 && kuManager.GetItemNumber(RM1name) < RM1num) return;
        if (RM2num != 0 && kuManager.GetItemNumber(RM2name) < RM2num) return;
        if (RM3num != 0 && kuManager.GetItemNumber(RM3name) < RM3num) return;
        Operationtime = time;
        if(RM1num!=0)kuManager.SubtractNumber(RM1name,RM1num);
        if(RM2num!=0)kuManager.SubtractNumber(RM2name,RM2num);
        if(RM3num!=0)kuManager.SubtractNumber(RM3name,RM3num);
        UpdateOperationStatus();
    }

}