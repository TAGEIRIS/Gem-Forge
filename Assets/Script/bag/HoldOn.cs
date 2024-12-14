using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldOn : MonoBehaviour
{
    void Awake()
    {
        // 确保不重复添加DontDestroyOnLoad对象
        if (FindObjectsOfType(typeof(HoldOn)).Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // 防止游戏对象在加载新场景时被销毁
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
