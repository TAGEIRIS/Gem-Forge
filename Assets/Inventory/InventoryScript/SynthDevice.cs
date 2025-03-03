using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SynthDevice", menuName = "Device/New SynthDevice")]
public class SynthDevice : ScriptableObject
{
    public int IndexNumber;
    public string Namefordesigner;

    //至多三个原料
    public string Gem1name;
    public int Gem1number;
    public string Gem2name;
    public string Gem2number;
    public string Gem3name;
    public string Gem3number;

    //至多2个产品
    public string Product1;
    public int Product1Number;
    public string Product2name;
    public int Product2number;

    //制作周期
    public int time;
}
