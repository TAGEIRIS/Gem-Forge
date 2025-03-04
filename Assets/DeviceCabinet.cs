using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeviceCabinet : MonoBehaviour
{
    public Transform[] devicesPositions;
    public DeviceKu deviceKu;
    private void Awake()
    {
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
        
    }
}
