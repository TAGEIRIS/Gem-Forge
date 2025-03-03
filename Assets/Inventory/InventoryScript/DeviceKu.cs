using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

[CreateAssetMenu(fileName = "New DeviceKu", menuName = "Device/New DeviceKu")]
public class DeviceKu : ScriptableObject
{
    //装置池
    public List<SynthDevice>DevicesPool = new List<SynthDevice>();
    //当前拥有的装置
    public List<SynthDevice>OwnDevices = new List<SynthDevice>();
}
