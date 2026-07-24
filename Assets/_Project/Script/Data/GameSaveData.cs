using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public PlayerSaveData playerSaveData = new PlayerSaveData();
    public DeviceSaveData deviceSaveData = new DeviceSaveData();

}

[System.Serializable]
public class PlayerSaveData
{
    
}


[System.Serializable]
public class DeviceRuntimeData
{
    public string deviceId;
    public int remainingTime;
}

[System.Serializable]
public class DeviceSaveData
{
    public List<string>ownDevice = new List<string>();//已拥有装置
    public List<DeviceRuntimeData> runningDevices = new List<DeviceRuntimeData>(); // 运行中的装置
}