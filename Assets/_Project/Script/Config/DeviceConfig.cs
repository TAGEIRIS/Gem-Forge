using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//装置配置
[CreateAssetMenu(fileName = "DeviceConfig", menuName = "Config/DeviceConfig")]
public class DeviceConfig : ScriptableObject
{
    //装置编号
    public string Id;
    public string displayName;
    public Sprite icon;
    public DeviceType deviceType;


    //装置本体
    public GameObject DevicePrefab;

    //装置的所需运行时间
    public int Operationtime;

    //装置的输入端
    public List<string> InputGemIds = new List<string>();
    //装置的输出端
    public List<string> OutputGemIds = new List<string>();
}