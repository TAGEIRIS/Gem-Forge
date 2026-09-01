using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DeviceSlot : MonoBehaviour
{
    List<GemSlot> gemSlots = new List<GemSlot>();
    public GameObject gemSlotPrefab; // 预制件引用
    public Transform inputGrid;
    public Transform outputGrid;


    //初始化装置面板信息
    public void Initialize(string deviceID)
    {
        DeviceConfig deviceConfig = GameConfig.Instance.GetDeviceConfigById(deviceID);

        foreach (var gemId in deviceConfig.InputGemIds)
        {
            GameObject gameObject = Instantiate(gemSlotPrefab, inputGrid);
            gameObject.transform.localPosition = Vector3.zero;

            GemSlot GemSlot = gameObject.GetComponent<GemSlot>();
            GemSlot.SetGemInfo(gemId);
            gemSlots.Add(GemSlot);
        }

        foreach (var gemId in deviceConfig.OutputGemIds)
        {
            GameObject gameObject = Instantiate(gemSlotPrefab, outputGrid);
            gameObject.transform.localPosition = Vector3.zero;

            GemSlot GemSlot = gameObject.GetComponent<GemSlot>();
            GemSlot.SetGemInfo(gemId);
            gemSlots.Add(GemSlot);
        }


    }


}
