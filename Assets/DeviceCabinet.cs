using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeviceCabinet : MonoBehaviour
{
    //位置的数组
    public Transform[] devicesPositions;

    public DeviceKu deviceKu;
    public KuManager kuManager;

    //字典记录当前已存储的装置及其位置索引
    public Dictionary<int, GameObject> equippedDevice = new
        Dictionary<int, GameObject>();

    private void Awake()
    {
        GameObject gameObject1 = GameObject.Find("KuManager");
        kuManager = gameObject1.GetComponent<KuManager>();

        GameObject gameObject = GameObject.Find("DevicesPosition");
        if (gameObject == null) return;

        // 获取所有子对象的Transform组件
        Transform[] allTransforms = gameObject.GetComponentsInChildren<Transform>();
        List<Transform> devicePositionList = new List<Transform>(allTransforms);

        // 移除自身，不添加到数组中
        devicePositionList.Remove(gameObject.transform);

        // 将List转换回数组
        devicesPositions = devicePositionList.ToArray();
    }

    private void Start()
    {
        UpdateCabnet();
    }

    public void UpdateCabnet()
    {
        if (deviceKu.OwnDevices.Count == 0) return;
        int num = 0;

        foreach(SynthDevice device in deviceKu.OwnDevices) 
        {
            EquipDevice(device, num++);
        }
    }
    public void EquipDevice(SynthDevice synthDevice, int index)
    {
        if (synthDevice != null && index >= 0 && index < devicesPositions.Length)
        {
            //实例化装置，并放置到预设位置
            GameObject device = Instantiate(synthDevice.Body,
                new Vector3(0, 0, 0), devicesPositions[index].rotation);
            device.transform.SetParent(devicesPositions[index], false);
            device.transform.localScale = new Vector3(1, 1, 1);
            //将装置与位置关联
            equippedDevice[index] = device;
        }
    }

}
