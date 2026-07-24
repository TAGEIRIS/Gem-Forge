using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DeviceSlot : MonoBehaviour
{
    List<GemInDeviceSlot> gemSlots = new List<GemInDeviceSlot>();
    public GameObject gemSlotPrefab; // 预制件引用
    public Transform inputGrid; 
    public Transform outputGrid;


    public void Initialize(string deviceID)
    {
        DeviceConfig deviceConfig = GameConfig.Instance.GetDeviceConfigById(deviceID);
        
        foreach(var gemId in deviceConfig.InputGemIds)
        {
            GameObject gameObject = Instantiate(gemSlotPrefab,inputGrid);
            gameObject.transform.localPosition = Vector3.zero;
            
            GemInDeviceSlot gemInDeviceSlot = gameObject.GetComponent<GemInDeviceSlot>();
            gemInDeviceSlot.SetGemInfo(gemId);
            gemSlots.Add(gemInDeviceSlot);
        }

        foreach(var gemId in deviceConfig.OutputGemIds)
        {
            GameObject gameObject = Instantiate(gemSlotPrefab,outputGrid);
            gameObject.transform.localPosition = Vector3.zero;
            
            GemInDeviceSlot gemInDeviceSlot = gameObject.GetComponent<GemInDeviceSlot>();
            gemInDeviceSlot.SetGemInfo(gemId);
            gemSlots.Add(gemInDeviceSlot);
        }


    }


}
