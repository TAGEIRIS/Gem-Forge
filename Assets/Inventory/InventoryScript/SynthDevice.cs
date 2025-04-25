using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SynthDevice", menuName = "Device/New SynthDevice")]
public class SynthDevice : ScriptableObject
{
    //装置编号
    public int IndexNumber;
    //装置名称(供开发者看)
    public string Namefordesigner;
    //装置本体
    public GameObject Body;

    //装置的状态
    //装置还需运作几天(每过一天天数减一，-1代表未运作)
    public int Operationtime;
}
