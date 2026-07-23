using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //面板
    //1号主界面
    public GameObject MainCanvas;
    //2号备战界面
    public GameObject BagCanvas;
    //3号工作界面
    public GameObject WorkbenchCanvas;
    //4号小镇界面
    public GameObject TownCanvas;

    public GameObject UnWeaponButtons;

    private void Awake()
    {
        if (BagCanvas == null)
        {
            BagCanvas = GameObject.Find("BagCanvas");
        }
        if (MainCanvas == null)
        {
            MainCanvas = GameObject.Find("MainCanvas");
        }
        if (WorkbenchCanvas == null)
        {
            WorkbenchCanvas = GameObject.Find("WorkbenchCanvas");
        }
        if (TownCanvas == null)
        {
            TownCanvas = GameObject.Find("TownCanvas");
        }
    }

    private void Start()
    {
        Transfer(1);
    }

    public void Transfer(int num)
    {
        MainCanvas.SetActive(false);
        BagCanvas.SetActive(false);
        WorkbenchCanvas.SetActive(false);
        TownCanvas.SetActive(false);
        if(num==1)MainCanvas.SetActive(true);
        else if(num==2)BagCanvas.SetActive(true);
        else if(num==3)WorkbenchCanvas.SetActive(true);
        else if(num==4)TownCanvas.SetActive(true);
        Debug.Log("场景"+num);
    }
    
}
