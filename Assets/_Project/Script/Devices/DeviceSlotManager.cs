using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSlotManager : MonoBehaviour
{
    //记录所有已运行装置的时间
    Dictionary<string,int>deviceRunningTime = new Dictionary<string, int>();

    //记录拥有的装置id
    List<string>deviceIds = new List<string>();

    public GameObject deviceSlotPrefab;

    public void Initialize()
    {
        
    }

    
}
