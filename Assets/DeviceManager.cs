using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public DeviceKu DeviceKu;

    public void Reset()
    {
        DeviceKu.OwnDevices.Clear();
    }
    public bool AddDevice(int deviceID)
    {
        foreach (SynthDevice aim in DeviceKu.DevicesPool)
        {
            if (aim.IndexNumber == deviceID)
            {
                foreach (SynthDevice aim1 in DeviceKu.OwnDevices)
                {
                    if (aim1.IndexNumber == deviceID) return false;
                }
                DeviceKu.OwnDevices.Add(aim);
                return true;
            }
        }
        return false;
    }
}
