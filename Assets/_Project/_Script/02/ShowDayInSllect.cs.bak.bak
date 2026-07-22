using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDayInSllect : MonoBehaviour
{
    //关卡选择器
    public LevelController LevelController;
    //日期文本
    public Text Text;

    //获取组件
    private void Awake()
    {
        GameObject gameObject = GameObject.Find("LevelController");
        LevelController = gameObject.GetComponent<LevelController>();
    }

    private void Start()
    {
        if (Text == null) Debug.Log("Fuck");
        Text.text = "第" + LevelController.currenWave.ToString() + "天";
    }
}
